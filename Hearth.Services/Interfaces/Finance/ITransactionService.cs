using System;
using System.Collections.Generic;
using System.Text;
using Hearth.Services.Interfaces;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Filters;

namespace Hearth.Services.Interfaces.Finance
{
    public interface ITransactionService : ISqliteTableService<TransactionDTO, SqliteTableFilter>
    {
        Task<TransactionDTO?> GetByTransactionId(string transactionId);
        Task DeleteTransactionsFromPlaidSyncRemoval(List<(string Account_Id, string Transaction_Id)> removed, bool saveChanges = true);
    }
}
