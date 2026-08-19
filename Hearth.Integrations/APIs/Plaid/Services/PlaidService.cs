using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Utility;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using Hearth.Integrations.APIs.Plaid.Interfaces;
using Hearth.Core.Data;
using Hearth.Integrations.APIs.Plaid.Mapping;

namespace Hearth.Integrations.APIs.Plaid.Services
{
    public class PlaidService : IPlaidService
    {
        private readonly HttpClient _httpClient;
        private readonly PlaidOptions _options;
        private readonly IBankService _bankService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IPlaidAccountService _plaidAccountService;
        private readonly HearthDbContext _context;

        private class LinkTokenResponse
        {
            public string link_token { get; set; } = default!;
            public string request_id { get; set; } = default!;
        }

        private class ExchangeToken
        {
            public string access_token { get; set; } = default!;
            public string item_id { get; set; } = default!;
            public string request_id { get; set; } = default!;
        }

        public PlaidService(
            HttpClient httpClient,
            IOptions<PlaidOptions> options,
            IBankService bankService,
            IAccountService accountService,
            ITransactionService transactionService,
            IPlaidAccountService plaidAccountService,
            HearthDbContext context
        )
        {
            _httpClient = httpClient;
            _options = options.Value;
            _bankService = bankService;
            _accountService = accountService;
            _transactionService = transactionService;
            _plaidAccountService = plaidAccountService;
            _context = context;
        }
        /// <summary>
        /// [/link/token/create] Takes a userId and creates a unqiue temporary "Plaid Link Token" for that user to connect to their bank institution.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>link_token</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<string?> CreateLinkTokenAsync(int userId)
        {
            var requestBody = new
            {
                client_id = _options.ClientId,
                secret = _options.Secret,
                user = new { client_user_id = userId.ToString() },
                client_name = "Hearth",
                products = new[] { "transactions" },
                country_codes = new[] { "US" },
                language = "en",
                link_customization_name = "default"
            };

            var response = await _httpClient.PostAsJsonAsync($"{_options.BaseUrl}/link/token/create", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error creating link token: {errorContent}");
            }

            var responseData = await response.Content.ReadFromJsonAsync<LinkTokenResponse>();
            return responseData?.link_token;
        }
        /// <summary>
        /// [/item/public_token/exchange] Takes the temporary "public token" aka "Plaid Link Token" generated when opening the Link Modal and exchanges it for a permanent access token, then stores the access token in the database.
        /// Access Tokens are attributed to a Bank and a User. The user can then use the access token to retrieve their accounts and transactions from that Bank, with the Access token.
        /// </summary>
        /// <param name="publicToken"></param>
        /// <param name="userId"></param>
        /// <returns>access_token</returns>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<BankDTO?> StoreAccessTokenAsync(string publicToken, int userId)
        {
            var requestBody = new
            {
                client_id = _options.ClientId,
                secret = _options.Secret,
                public_token = publicToken
            };

            var response = await _httpClient.PostAsJsonAsync($"{_options.BaseUrl}/item/public_token/exchange", requestBody);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error exchanging public token: {errorContent}");
            }

            var responseData = await response.Content.ReadFromJsonAsync<ExchangeToken>();
            if (responseData is null) return null;

            var accountsResponseData = await _plaidAccountService.GetFreshBankAccountsWithItem(responseData.access_token);
            
            //if(accountsResponseData == null) throw new HearthRecordNotFoundException(responseData.access_token);

            // IMPORTANT
            // - REMEMBER a bank is simply a users relationship to an institution. DO NOT think of it as an actual bank.
            var bank = new BankDTO
            {
                Item_Id = responseData.item_id,
                Access_Token = responseData.access_token,
                UserId = userId,
                Request_Id = responseData.request_id,
                Institution_Id = accountsResponseData.Item?.Institution_Id,
                LastDateRequested = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                InitalDateRequested = DateTime.UtcNow,
            };


            var accounts = accountsResponseData.Accounts.ToDtoList();
            // this prob isn't the cleanest way to do this but i dont care rn
            accounts.Select(a => a.Bank_Item_Id = accountsResponseData.Item.Item_Id).ToList();

            try
            {
                await _bankService.Create(bank, false);
                await _accountService.CreateRange(accounts, false);
            }
            catch
            {
                // TODO
                throw;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                // TODO
                throw;
            }

            return bank;
        }
    }
}