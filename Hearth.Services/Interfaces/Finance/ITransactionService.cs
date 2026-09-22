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
        /// Gets all transactions with their associated account and category information. This must be a unique method, instead of just GetAll with some inclusions, because that would cause recursion
        /// </summary>
        /// <returns></returns>
        Task<List<TransactionDTO>?> GetAllWithAccountAndCategory();
        Task DeleteTransactionsFromPlaidSyncRemoval(List<(string Account_Id, string Transaction_Id)> removed, bool saveChanges = true);
    }
}
