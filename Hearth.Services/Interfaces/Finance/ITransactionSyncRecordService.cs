using Hearth.Services.DTOs.Finance.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces.Finance
{
    public interface ITransactionSyncRecordService : ISqliteTableService<TransactionSyncRecordDTO>
    {
        Task<TransactionSyncRecordDTO> GetNextByItemId(string itemId);
    }
}
