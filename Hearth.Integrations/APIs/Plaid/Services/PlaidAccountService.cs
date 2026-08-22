using Hearth.Core.Data;
using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Integrations.APIs.Plaid.Interfaces;
using Hearth.Integrations.APIs.Plaid.Mapping;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Utility;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    
        public async Task<List<AccountDTO>?> CreateAndUpdateAllAccounts(IProgress<PlaidSyncProgress>? progress = null)
        {
            List<AccountDTO> totalAdded = new List<AccountDTO>();

            var allBanks = await _bankService.GetAll();
            var hearthAccounts = await _accountService.GetAll();

            for (int i = 0; i < allBanks.Count; i++)
            {
                var bank = allBanks[i];

                progress?.Report(new PlaidSyncProgress
                {
                    CurrentBankIndex = i + 1,
                    TotalBanks = allBanks.Count,
                    CurrentInstitution = bank.Institution_Id,
                    Stage = "Fetching accounts"
                });

                var bankAccounts = await GetBankAccounts(bank.Item_Id);

                // if no bankAccounts are found, just skip to the next iteration
                if (bankAccounts == null || bankAccounts.Count == 0) continue;

                // Build a fast lookup of Plaid account IDs already in the database
                var existingAccountIds = hearthAccounts
                    .Select(a => a.Account_Id)
                    .ToHashSet();

                var newAccounts = bankAccounts
                    .Where(ba => !existingAccountIds.Contains(ba.Account_Id))
                    .ToList();

                var updateAccounts = bankAccounts
                    .Where(ba => existingAccountIds.Contains(ba.Account_Id))
                    .ToList();


                if (newAccounts != null && newAccounts.Count >= 1)
                {
                    progress?.Report(new PlaidSyncProgress
                    {
                        CurrentBankIndex = i + 1,
                        TotalBanks = allBanks.Count,
                        CurrentInstitution = bank.Institution_Id,
                        Stage = $"Creating {newAccounts.Count} new accounts from {bank.Item_Id}"
                    });
                    await _accountService.CreateRange(newAccounts, false);
                }

                if(updateAccounts != null && updateAccounts.Count >= 1)
                {
                    progress?.Report(new PlaidSyncProgress
                    {
                        CurrentBankIndex = i + 1,
                        TotalBanks = allBanks.Count,
                        CurrentInstitution = bank.Institution_Id,
                        Stage = $"Updating {updateAccounts.Count} accounts from {bank.Item_Id}"
                    });
                    await _accountService.UpdateRange(updateAccounts, false);
                }

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                // TODO
            }

            return totalAdded;
        }
    }
}
