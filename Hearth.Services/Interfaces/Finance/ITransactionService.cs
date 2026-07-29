using System;
using System.Collections.Generic;
using System.Text;
using Hearth.Services.Interfaces;
using Hearth.Services.DTOs.Finance.Transaction;

namespace Hearth.Services.Interfaces.Finance
{
    public interface ITransactionService : ISqliteTableService<TransactionDTO>
    {
        Task<TransactionDTO?> GetByTransactionId(string transactionId);
    }
}
