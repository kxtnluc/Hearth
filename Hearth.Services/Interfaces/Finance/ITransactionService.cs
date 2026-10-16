using System;
using System.Collections.Generic;
using System.Text;
using Hearth.Services.Interfaces;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Filters;
using Hearth.Services.Filters.Finance;

namespace Hearth.Services.Interfaces.Finance
{
    public interface ITransactionService : ISqliteTableService<TransactionDTO, TransactionFilter>
    {
        Task<TransactionDTO?> GetByTransactionId(string transactionId);
        Task<List<TransactionDTO>?> GetByAccountId(string accountId);
        /// <summary>
        /// Gets all transactions with their associated category information.
        /// </summary>
        /// <returns></returns>
        Task<List<TransactionDTO>> GetAllWithCategory(int[]? transactionIds = null);
        Task DeleteTransactionsFromPlaidSyncRemoval(List<(string Account_Id, string Transaction_Id)> removed, bool saveChanges = true);
        /// <summary>
        /// Updates just the category id of a single transaction. This was made since this is called often, and don't need to update the whole row all the time.
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="categoryid"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task UpdateCategoryId(int transactionId, int categoryid, bool saveChanges = true);
    }
}
