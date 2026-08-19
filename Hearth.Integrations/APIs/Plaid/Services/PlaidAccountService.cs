using Hearth.Core.Data;
using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Integrations.APIs.Plaid.Interfaces;
using Hearth.Integrations.APIs.Plaid.Mapping;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Utility;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Services
{
    internal class PlaidAccountService : IPlaidAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly PlaidOptions _options;
        private readonly IBankService _bankService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionSyncRecordService _transactionSyncRecordService;
        private readonly HearthDbContext _context;


        public PlaidAccountService(
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

        public async Task<List<AccountDTO>?> GetBankAccounts(string itemId)
        {
            var foundBank = await _bankService.GetByItemId(itemId);

            if (foundBank == null) throw new HearthRecordNotFoundException("While trying to grab Accounts, the Bank was not found: " + itemId);

            var requestBody = new
            {
                client_id = _options.ClientId,
                secret = _options.Secret,
                access_token = foundBank.Access_Token,
            };

            // - The Call -
            var response = await _httpClient.PostAsJsonAsync($"{_options.BaseUrl}/accounts/get", requestBody);

            // - Exception Handling -
            if (response.IsSuccessStatusCode == false)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Error getting accounts from Plaid: " + errorContent);
            }

            var responseData = await response.Content.ReadFromJsonAsync<PlaidAccountGetHttpResponse>();

            // - Exception Handling -
            if (responseData.Request_Id == null)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Missing data returned from accounts/get: " + errorContent);
            }

            var accounts = responseData.Accounts!.ToDtoList();

            if (accounts == null) throw new HearthRecordNotFoundException();

            return accounts;
        }

        public async Task<PlaidAccountGetHttpResponse> GetFreshBankAccountsWithItem(string accessToken)
        {
            var requestBody = new
            {
                client_id = _options.ClientId,
                secret = _options.Secret,
                access_token = accessToken,
            };

            // - The Call -
            var response = await _httpClient.PostAsJsonAsync($"{_options.BaseUrl}/accounts/get", requestBody);

            // - Exception Handling -
            if (response.IsSuccessStatusCode == false)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Error getting accounts from Plaid: " + errorContent);
            }

            var responseData = await response.Content.ReadFromJsonAsync<PlaidAccountGetHttpResponse>();

            // - Exception Handling -
            if (responseData.Request_Id == null)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException("Missing data returned from accounts/get: " + errorContent);
            }

            return responseData;
        }
    }
}
