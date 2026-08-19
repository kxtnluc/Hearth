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

        public async Task<PlaidTransactionSyncHttpResponse> SyncBankTransactions(string itemId, int recordsToSync = 50)
        {
            BankDTO? foundBank;
            string nextCursor;

            try
            {
                foundBank = await _bankService.GetByItemId(itemId);
            }
            catch (HearthRecordNotFoundException ex)
            {
                // ERROR HANDLING
                throw;
            }

            try
            {
                TransactionSyncRecordDTO tsr = await _transactionSyncRecordService.GetNextByItemId(itemId);
                nextCursor = tsr.Next_Cursor;
            }
            catch(HearthRecordNotFoundException)
            {
                nextCursor = string.Empty;
            }

            var requestBody = new
            {
                client_id = _options.ClientId,              // Your Plaid client_id
                secret = _options.Secret,                   // Your Plaid secret
                access_token = foundBank?.Access_Token,     // The token to access a specific bank
                cursor = nextCursor,                        // A random string that tells the query what transactions have already been fetched before
                count = recordsToSync,                      // How Many Transactions to pull
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
                await _transactionService.CreateRange(addedTransactions, false);
                await _transactionService.UpdateRange(modifiedTransactions, false);
                //await _transactionService.DeleteRange(removedTransactions, false) TODO low priority
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
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }

            return responseData;
        }
    }
}
