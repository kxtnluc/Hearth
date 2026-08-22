using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Filters;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Hearth.Services.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Services.Finance
{
    public class TransactionSyncRecordService : ASqliteTableService<TransactionSyncRecord, TransactionSyncRecordDTO, SqliteTableFilter>, ITransactionSyncRecordService
    {
        public TransactionSyncRecordService(HearthDbContext context) : base(context) { }
        #region Abstract Class Setup
        protected override DbSet<TransactionSyncRecord> DbSet => _context.TransactionSyncRecords;
        protected override TransactionSyncRecordDTO ToDto(TransactionSyncRecord entity) => entity.ToDto();
        protected override TransactionSyncRecord ToEntity(TransactionSyncRecordDTO dto) => dto.ToEntity();
        protected override List<TransactionSyncRecordDTO> ToDtoList(List<TransactionSyncRecord> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(TransactionSyncRecordDTO dto, TransactionSyncRecord entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(TransactionSyncRecordDTO payload)
        {
            if(payload == null) throw new HearthInvalidPayloadException();
            if(payload.Item_Id == null || payload.Request_Id == null) throw new HearthInvalidPayloadException();

            return;
        }

        #endregion

        #region Filter
        public override List<TransactionSyncRecordDTO> Filter(List<TransactionSyncRecordDTO> banks, SqliteTableFilter filter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions
        public async Task<TransactionSyncRecordDTO> GetNextByItemId(string itemId)
        {
            var tsr = await _context.TransactionSyncRecords
                .Where(t => t.Item_Id == itemId)
                .OrderByDescending(t => t.Write_Date)
                .FirstOrDefaultAsync();

            if (tsr == null)
            {
                throw new HearthRecordNotFoundException(itemId);
            }

            return tsr.ToDto();
        }
        #endregion
    }
}
