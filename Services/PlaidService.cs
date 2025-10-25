using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Hearth.Data;
using Hearth.Models;
using Hearth.Models.PTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DotNetEnv;

namespace Hearth.Services
{
    public class PlaidService
    {
        #region Variables
        #region A: Global Service Variables
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly HearthDbContext _dbContext;
        #endregion
        #region B: Keys
        #endregion
        #region A: Temp Classes
        private class LinkTokenResponse
        {
            public string link_token { get; set; }
            public string request_id { get; set; }
        }

        private class ExchangeToken
        {
            public string access_token { get; set; }
            public string item_id { get; set; }
            public string request_id { get; set; }
        }
        #endregion
        #region A: Injections
        public PlaidService(HttpClient httpClient, IConfiguration configuration, HearthDbContext dbContext)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _dbContext = dbContext;
        }
        #endregion
        #endregion
        #region API Calls
        #region A: Plaid Setup Calls
        #region link/token/create
        // creates a temporary Link Token for a user
        public async Task<string> CreateLinkTokenAsync(int userId)
        {
            Env.Load(); // Load environment variables from .env file
            var requestBody = new
            {
                client_id = Environment.GetEnvironmentVariable("PLAID_CLIENT_ID"),
                secret = Environment.GetEnvironmentVariable("PLAID_PRODUCTION_KEY"),
                user = new { client_user_id = userId.ToString() },
                client_name = "Hearth",
                products = new[] { "transactions" },
                country_codes = new[] { "US" },
                language = "en",
                link_customization_name = "default"
            };

            // Use the sandbox environment for development
            var response = await _httpClient.PostAsJsonAsync("https://production.plaid.com/link/token/create", requestBody);
            if (response.IsSuccessStatusCode)
            {
                // Deserialize into the strongly-typed model
                var responseData = await response.Content.ReadFromJsonAsync<LinkTokenResponse>();
                return responseData?.link_token;
            }
            else
            {
                // Handle error response here, e.g., log it or throw an exception
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error creating link token: {errorContent}");
            }
        }
        #endregion
        // ----------------------------------------------------------------------------
        #region item/public_token/exchange
        // exchange the temporary Public Link Token (from link/token/create) for a Permenant Access Token to a bank institution
        public async Task<string> StoreAccessTokenAsync(string p_publicToken, int p_userId)
        {
            Env.Load(); // Load environment variables from .env file
            // Setup Request Body and Service
            using var httpClient = new HttpClient();
            var requestBody = new
            {
                client_id = Environment.GetEnvironmentVariable("PLAID_CLIENT_ID"),
                secret = Environment.GetEnvironmentVariable("PLAID_PRODUCTION_KEY"),
                public_token = p_publicToken
            };
            // The Call
            var response = await httpClient.PostAsJsonAsync("https://production.plaid.com/item/public_token/exchange", requestBody);
            // Reading the Response
            var responseData = await response.Content.ReadFromJsonAsync<ExchangeToken>();

            // Sets Each Data Point
            var accessToken = responseData?.access_token;
            var itemId = responseData?.item_id;
            var requestId = responseData?.request_id;
            Bank bank = new Bank
            {
                Id = itemId,
                UserId = p_userId,
                Access_Token = accessToken,
                Request_Id = requestId,
            };

            // Fetch Bank Account Data (this also adds them to the database)

            // Save Access Token to DB

            await CreateAndUpdateAccounts(bank.Access_Token, p_userId);

            _dbContext.Banks.Add(bank);
            await _dbContext.SaveChangesAsync();

            return accessToken;
        }
        #endregion
        #endregion
        #region B: Plaid Data Calls

        #region transactions/sync
        public async Task<List<Transaction>>? SyncTransactionsAsync(string p_bankId) // Remember that this syncs ALL a banks transactions across all accounts!!!!
        {
        // <start>
            #region A: GET needed db data

            var bank = _dbContext.Banks
                .SingleOrDefault(b => b.Id == p_bankId);

            var categories = CategoryFunctions.GetAll(_dbContext);

            var rules = CategoryOrganizationRuleFunctions.GetAll(_dbContext);

            if (bank == null) return null; // or maybe use empty list: new List<Transaction>()

            #endregion
            #region B: GET db.TransactionSyncRecords Data

            // Get last sync Curosr value (which determines which transactions to fetch)
            var mostRecentSyncRecord = _dbContext.TransactionSyncRecords
                .OrderByDescending(t => t.Write_Date)
                .Where(t => t.Bank_Id == p_bankId)
                .FirstOrDefault();

            var foundCursor = mostRecentSyncRecord?.Next_Cursor ?? string.Empty;

            #endregion
            #region C: FETCH/SET Plaid Transaction/Sync
                #region 01: Setup/Execute FETCH
            Env.Load(); // Load environment variables from .env file
            var requestBody = new
            {
                client_id = Environment.GetEnvironmentVariable("PLAID_CLIENT_ID"),      // Your Plaid client_id
                secret = Environment.GetEnvironmentVariable("PLAID_PRODUCTION_KEY"),    // Your Plaid secret
                access_token = bank.Access_Token,           // The token to access a specific bank
                cursor = foundCursor,                       // A random string that tells the query what transactions have already been fetched before
                count = 50,                                 // How Many Transactions to pull
                //days_requested = 90                       // How Far Back the transaction query will go. 90 Days is the default
            };
            var response = await _httpClient.PostAsJsonAsync("https://production.plaid.com/transactions/sync", requestBody);
                #endregion 01
                #region 02: Process Data
            if (response.IsSuccessStatusCode)
            {
                // Proccess Data from HTTP to JSON
                var responseData = await response.Content.ReadFromJsonAsync<TransactionSyncResponsePTO>();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (responseData?.Added != null && responseData?.Modified != null && responseData?.Removed != null)
                {
                    #region 02_A: Generate C# Models from JSON
                    List<Transaction> addedTransactions = new List<Transaction> { };
                    //List<Transaction> modifiedTransactions = new List<Transaction> { };

                    addedTransactions = responseData.Added.Select(t => 
                    new Transaction
                    {
                        Id = t.Transaction_Id,
                        Name = t.Name,
                        Amount = t.Amount,
                        Date = t.Date,
                        AccountId = t.Account_Id,
                        Account_Owner = t.Account_Owner,
                        Authorized_Date = t.Authorized_Date,
                        Authorized_Datetime = t.Authorized_Datetime,
                        Plaid_Category_Id = t.Category_Id,
                        Check_Number = t.Check_Number,
                        Datetime = t.Datetime,
                        Iso_Currency_Code = t.Iso_Currency_Code,
                        Logo_Url = t.Logo_Url,
                        Merchant_Entity_Id = t.Merchant_Entity_Id,
                        Merchant_Name = t.Merchant_Name,
                        Payment_Channel = t.Payment_Channel,
                        Pending = t.Pending,
                        Pending_Transaction_Id = t.Pending_Transaction_Id,
                        Personal_Finance_Category_Icon_Url = t.Personal_Finance_Category_Icon_Url,
                        Transaction_Code = t.Transaction_Code,
                        Transaction_Type = t.Transaction_Type,
                        Unofficial_Currency_Code = t.Unofficial_Currency_Code,
                        Website = t.Website,
                        Personal_Finance_Category_Primary = t.Personal_Finance_Category?.Primary,
                        Personal_Finance_Category_Detailed = t.Personal_Finance_Category?.Detailed,
                        Personal_Finance_Category_Confidence_Level = t.Personal_Finance_Category?.Confidence_Level,
                        CategoryId = 0
                    }).ToList();

                    // maybe do modified
                    //  here---

                    // Create Timestamps for some data before posting
                    #endregion
                    #region 02_B: Generate new Sync Record
                    var syncRecord = new TransactionSyncRecord
                    {
                        Next_Cursor = responseData.Next_Cursor,
                        Has_More = responseData.Has_More,
                        Request_Id = responseData.Request_Id,
                        Transactions_Update_Status = responseData.Transactions_Update_Status,
                        Bank_Id = bank.Id,
                        Write_Date = DateTime.UtcNow
                    };
                    #endregion
                    #region 02_C: SET & RETURN Data
                    try
                    {
                        _dbContext.TransactionSyncRecords.Add(syncRecord);
                        _dbContext.Transactions.AddRange(addedTransactions);
                        TransactionFunctions.AutoAssignCategories(addedTransactions, categories, rules);
                        await _dbContext.SaveChangesAsync();
                    }
                    catch (DbUpdateException dbEx)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + dbEx.Message);
                        System.Diagnostics.Debug.WriteLine("Inner exception: " + dbEx.InnerException?.Message);
                        System.Diagnostics.Debug.WriteLine("Stack Trace: " + dbEx.StackTrace);
                        throw;
                    }
                    return addedTransactions;
                    #endregion
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No transactions found.");
                    return null;
                }
            }
                #endregion 02
                #region 03: FETCH Error Handling
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error fetching transactions: {errorContent}");
                return null;
            }
            #endregion 03
            #endregion C
        // <end>
        }
        #endregion

        #region accounts/get
        public async Task<List<Account>>? CreateAndUpdateAccounts(string bankId, int? userId)
        {
            Env.Load();
            var foundBank = _dbContext.Banks.SingleOrDefault(b => b.Id == bankId);

            if(foundBank == null)
            {
                return null;
            }

            var requestBody = new
            {
                client_id = Environment.GetEnvironmentVariable("PLAID_CLIENT_ID"),      // Your Plaid client_id
                secret = Environment.GetEnvironmentVariable("PLAID_PRODUCTION_KEY"),    // Your Plaid secret
                access_token = foundBank.Access_Token,
            };

            // Sending the request to Plaid API
            var response = await _httpClient.PostAsJsonAsync("https://production.plaid.com/accounts/get", requestBody);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadFromJsonAsync<AccountResponsePTO>();
                var responseContent = await response.Content.ReadAsStringAsync();
                // Process the response
                if (responseData != null)
                {
                    List<Account> foundAccounts = new List<Account> { };

                    // Conjure the DateTimes (hehe)
                    var lastModified = DateTime.UtcNow;
                    var lastDateRequested = DateTime.UtcNow;

                    foundAccounts = responseData.Accounts.Select(a => new Account
                    {
                        Id = a.AccountId,
                        BankId = bankId,
                        Mask = a.Mask,
                        Name = a.Name,
                        Offical_Name = a.OfficialName,
                        Type = a.Type,
                        Subtype = a.Subtype,
                        UserId = userId ?? 1,
                        Institution_Id = responseData.Item.InstitutionId,
                        Institution_Name = responseData.Item.InstitutionName,
                        Item_Id = responseData.Item.ItemId,
                        Request_Id = responseData.RequestId,
                        Last_Date_Requested = lastDateRequested,
                        Last_Modified = lastModified,
                        Balance_Available = a.Balances.Available,
                        Balance_Current = a.Balances.Current
                    }).ToList();

                    // Add Missing Accounts to the DB
                    try
                    {
                        var existingAccountIds = _dbContext.Accounts.Select(a => a.Id).ToHashSet();

                        var missingAccounts = foundAccounts
                            .Where(fa => !existingAccountIds.Contains(fa.Id))
                            .Select(fa =>
                            {
                                fa.Inital_Date_Requested = DateTime.UtcNow;
                                return fa;
                            })
                            .ToList();

                        if (missingAccounts != null && missingAccounts.Count >= 1)
                        {
                        // If missing accounts are found, add them
                            _dbContext.Accounts.AddRange(missingAccounts);
                            await _dbContext.SaveChangesAsync();
                        }
                        // Update already existing accounts
                        if (existingAccountIds.Any())
                        {
                            try
                            {
                                var foundExistingAccounts = foundAccounts
                                    .Where(fa => existingAccountIds.Contains(fa.Id))
                                    .ToList();
                                foreach (var a in foundExistingAccounts)
                                {
                                    var accountToUpdate = _dbContext.Accounts.FirstOrDefault(atu => atu.Id == a.Id);
                                    if (accountToUpdate != null)
                                    {
                                        accountToUpdate.BankId = a.BankId;
                                        accountToUpdate.Mask = a.Mask;
                                        accountToUpdate.Name = a.Name;
                                        accountToUpdate.Offical_Name = a.Offical_Name;
                                        accountToUpdate.Type = a.Type;
                                        accountToUpdate.Subtype = a.Subtype;
                                        accountToUpdate.UserId = a.UserId;
                                        accountToUpdate.Institution_Id = a.Institution_Id;
                                        accountToUpdate.Institution_Name = a.Institution_Name;
                                        accountToUpdate.Item_Id = a.Item_Id;
                                        accountToUpdate.Request_Id = a.Request_Id;
                                        accountToUpdate.Last_Date_Requested = a.Last_Date_Requested;
                                        accountToUpdate.Last_Modified = a.Last_Modified;
                                        accountToUpdate.Balance_Available = a.Balance_Available;
                                        accountToUpdate.Balance_Current = a.Balance_Current;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine("Something went wrong when Setting Account Data: ",ex.Message);
                            }
                            try
                            {
                                await _dbContext.SaveChangesAsync();
                            }
                            catch (Exception ex)
                            {
                                System.Diagnostics.Debug.WriteLine("Something went wrong when Updating DB Accounts Data: ", ex.Message);
                            }
                        }
                    }
                    catch (DbUpdateException dbEx)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + dbEx.Message);
                        System.Diagnostics.Debug.WriteLine("Inner exception: " + dbEx.InnerException?.Message);
                        System.Diagnostics.Debug.WriteLine("Stack Trace: " + dbEx.StackTrace);
                        throw;
                    }

                    return foundAccounts;
                }
            }

            return null;
        }
        #endregion

        #endregion
        #endregion

        #region Major Sync
        public async Task MajorSync()
        {
            
            List<Bank> allBanks = new List<Bank>();

            allBanks = _dbContext.Banks.ToList();

            foreach (var b in allBanks)
            {
                if(b != null && b.Id != null)
                {
                    // Sync all Transactions
                    var tResult = await SyncTransactionsAsync(b.Id);
                    // Sync all Accounts
                    var aResult = await CreateAndUpdateAccounts(b.Id, null);
                }
            }

            
        }
        #endregion
    }
}
