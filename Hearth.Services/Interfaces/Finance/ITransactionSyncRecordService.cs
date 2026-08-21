using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces.Finance
{
    public interface ITransactionSyncRecordService : ISqliteTableService<TransactionSyncRecordDTO, SqliteTableFilter>
    {
        Task<TransactionSyncRecordDTO> GetNextByItemId(string itemId);
    }
}
