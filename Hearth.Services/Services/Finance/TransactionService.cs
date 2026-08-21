using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Filters;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Microsoft.EntityFrameworkCore;
namespace Hearth.Services.Services.Finance
{
    public class TransactionService : ASqliteTableService<Transaction, TransactionDTO, SqliteTableFilter>, ITransactionService
    {
        public TransactionService(HearthDbContext context) : base(context) { }
        #region Abstract Class Setup
        protected override DbSet<Transaction> DbSet => _context.Transactions;
        protected override TransactionDTO ToDto(Transaction entity) => entity.ToDto();
        protected override Transaction ToEntity(TransactionDTO dto) => dto.ToEntity();
        protected override List<TransactionDTO> ToDtoList(List<Transaction> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(TransactionDTO dto, Transaction entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(TransactionDTO payload)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Filter
        public override List<TransactionDTO> Filter(List<TransactionDTO> banks, SqliteTableFilter filter)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions
        public async Task<TransactionDTO?> GetByTransactionId(string transactionId)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(a => a.Transaction_Id == transactionId);

            return transaction?.ToDto();
        }

        public async Task DeleteTransactionsFromPlaidSyncRemoval(List<(string Account_Id, string Transaction_Id)> removed, bool saveChanges = true)
        {
            if (removed.Count == 0) return;

            var transactionIds = removed.Select(r => r.Transaction_Id).ToList();

            var entities = await DbSet
                .Where(t => transactionIds.Contains(t.Transaction_Id))
                .ToListAsync();

            if (entities.Count == 0) return;

            DbSet.RemoveRange(entities);
            if (saveChanges) await _context.SaveChangesAsync();
        }
        #endregion
    }
}