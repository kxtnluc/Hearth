using Hearth.Core.Models.Finance;
using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.DTOs.Finance.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Interfaces
{
    public interface IPlaidTransactionService
    {
        /// <summary>
        /// Adds, Updates, and Deletes transactions at one bank
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="recordsToSync"></param>
        /// <returns>Sync Suummary</returns>
        Task<PlaidSyncSummary> SyncBankTransactions(string itemId, int recordsToSync = 250, bool saveChanges = true, IProgress<PlaidSyncProgress>? progress = null);
        /// <summary>
        /// Adds, Updates, and Deletes Transactions across banks passed.
        /// </summary>
        /// <param name="banks"></param>
        /// <param name="recordsToSync"></param>
        /// <returns>Sync Suummary</returns>
        Task<PlaidSyncSummary> SyncRangeBanksTransactions(List<BankDTO>? banks, int recordsToSync = 250, bool saveChanges = true, IProgress<PlaidSyncProgress>? progress = null);
        /// <summary>
        /// Adds, Updates, and Deletes Transactions across ALL banks in the database.
        /// </summary>
        /// <param name="recordsToSync"></param>
        /// <param name="saveChanges"></param>
        /// <param name="progress"></param>
        /// <returns>Sync Suummary</returns>
        Task<PlaidSyncSummary> SyncAllBanksTransactions(int recordsToSync = 250, bool saveChanges = true, IProgress<PlaidSyncProgress>? progress = null);
    }
}
