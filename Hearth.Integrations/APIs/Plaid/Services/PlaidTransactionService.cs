using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Integrations.APIs.Plaid.Interfaces;
using Hearth.Integrations.APIs.Plaid.Mapping;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Utility;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Services
{
    public class PlaidTransactionService : IPlaidTransactionService
    {
        private readonly HttpClient _httpClient;
        private readonly PlaidOptions _options;
        private readonly IBankService _bankService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionSyncRecordService _transactionSyncRecordService;
        private readonly HearthDbContext _context;


        public PlaidTransactionService(
            HttpClient httpClient,
            IOptions<PlaidOptions> options,
            IBankService bankService,
            IAccountService accountService,
            ITransactionService transactionService,
            ITransactionSyncRecordService transactionSyncRecordService,
            HearthDbContext context
        )
        {
            _httpClient = httpClient;
            _options = options.Value;
            _bankService = bankService;
            _accountService = accountService;
            _transactionService = transactionService;
            _transactionSyncRecordService = transactionSyncRecordService;
            _context = context;
        }

        public async Task<PlaidSyncSummary> SyncBankTransactions(string itemId, int recordsToSync = 250, bool saveChanges = true, IProgress<PlaidSyncProgress>? progress = null)
        {
            BankDTO? foundBank;

            progress?.Report(new PlaidSyncProgress
            {
                Stage = $"Fetching Bank"
            });

            try
            {
                foundBank = await _bankService.GetByItemId(itemId);
            }
            catch (HearthRecordNotFoundException ex)
            {
                // ERROR HANDLING
                throw;
            }

            string nextCursor;
            try
            {
                TransactionSyncRecordDTO tsr = await _transactionSyncRecordService.GetNextByItemId(itemId);
                nextCursor = tsr.Next_Cursor;
            }
            catch(HearthRecordNotFoundException)
            {
                nextCursor = string.Empty;
            }

            var summary = new PlaidSyncSummary();
            bool hasMore = true;
            int page = 0;
            const int maxPages = 100;

            while (hasMore && page < maxPages)
            {
                page++;
                progress?.Report(new PlaidSyncProgress
                {
                    Stage = $"Syncing transactions (page {page})",
                    CurrentInstitution = foundBank?.Institution_Id
                });

                var requestBody = new
                {
                    client_id = _options.ClientId,              // Your Plaid client_id
                    secret = _options.Secret,                   // Your Plaid secret
                    access_token = foundBank?.Access_Token,     // The token to access a specific bank
                    cursor = nextCursor,                        // A random string that tells the query what transactions have already been fetched before
                    count = recordsToSync,                      // How Many Transactions to pull, 250 default
                    //days_requested = 90,                      // How Far Back the transaction query will go. 90 Days is the default
                };

                // - The Call -
                var response = await _httpClient.PostAsJsonAsync($"{_options.BaseUrl}/transactions/sync", requestBody);

                // - Exception Handling -
                if (response.IsSuccessStatusCode == false)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException("Error syncing transactions: " + errorContent);
                }

                var responseData = await response.Content.ReadFromJsonAsync<PlaidTransactionSyncHttpResponse>();

                // - Exception Handling -
                if (responseData.Added == null || responseData.Modified == null || responseData.Removed == null)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException("Missing data returned from syncing transactions: " + errorContent);
                }
                // - Mapping -
                var addedTransactions = responseData.Added.ToDtoList();
                var modifiedTransactions = responseData.Modified.ToDtoList();
                var removedTransactions = responseData.Removed
                    .Select(r => (r.Account_Id, r.Transaction_Id))
                    .ToList();
                var syncRecord = new TransactionSyncRecordDTO
                {
                    Next_Cursor = responseData.Next_Cursor,
                    Has_More = responseData.Has_More,
                    Request_Id = responseData.Request_Id,
                    Transactions_Update_Status = responseData.Transactions_Update_Status,
                    Item_Id = itemId,
                    // Write date is auto set. i think
                };
                // - Database -
                try
                {
                    await _transactionSyncRecordService.Create(syncRecord, false);
                    if (addedTransactions.Count >= 1) await _transactionService.CreateRange(addedTransactions, false);
                    if (modifiedTransactions.Count >= 1) await _transactionService.UpdateRange(modifiedTransactions, false);
                    if (removedTransactions.Count >= 1) await _transactionService.DeleteTransactionsFromPlaidSyncRemoval(removedTransactions, false);
                }
                catch(HearthInvalidPayloadException ex)
                {
                    throw new($@"
                        Invalid Paylaod Exception ):
                        Error: {ex.Message}
                        Inner exception: {ex.InnerException?.Message}
                        Stack Trace: {ex.StackTrace}
                    ");
                }
                // Since there is a lot of db stuff going on, we manually call 1 "SaveChanges", instead of calling it 4+ times for each call above.

                summary.TotalAdded += addedTransactions.Count;
                summary.TotalModified += modifiedTransactions.Count;
                summary.TotalRemoved += removedTransactions.Count;

                // Feed this page's cursor into the next request
                nextCursor = responseData.Next_Cursor;
                hasMore = responseData.Has_More;
            }

            try
            {
                if (saveChanges)
                {
                    progress?.Report(new PlaidSyncProgress
                    {
                        Stage = $"Writing to Database"
                    });
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // TODO
                throw;
            }

            return summary;
        }

        public async Task<PlaidSyncSummary> SyncRangeBanksTransactions(List<BankDTO>? banks, int recordsToSync = 250, bool saveChanges = true, IProgress<PlaidSyncProgress>? progress = null)
        {

            PlaidSyncSummary summary = new();

            foreach (var bank in banks)
            {
                var bankSyncSummary = await SyncBankTransactions(bank.Item_Id, recordsToSync, false);
                
                summary.TotalAdded += bankSyncSummary.TotalAdded;
                summary.TotalModified += bankSyncSummary.TotalModified;
                summary.TotalRemoved += bankSyncSummary.TotalRemoved;
            }

            if(saveChanges) await _context.SaveChangesAsync();

            return summary;
        }

        public async Task<PlaidSyncSummary> SyncAllBanksTransactions(int recordsToSync = 250, bool saveChanges = true, IProgress<PlaidSyncProgress>? progress = null)
        {
            List<BankDTO> allBanks = new List<BankDTO>();
            PlaidSyncSummary summary = new();

            allBanks = await _bankService.GetAll();
            summary = await SyncRangeBanksTransactions(allBanks, recordsToSync, false);

            if (saveChanges) await _context.SaveChangesAsync();

            return summary;

        }
    }
}