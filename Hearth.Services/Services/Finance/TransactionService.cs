using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Microsoft.EntityFrameworkCore;
namespace Hearth.Services.Services.Finance
{
    public class TransactionService : ASqliteTableService<Transaction, TransactionDTO>, ITransactionService
    {
        public TransactionService(HearthDbContext context) : base(context) { }
        #region Abstract Class Setup
        protected override DbSet<Transaction> DbSet => _context.Transactions;
        protected override TransactionDTO ToDto(Transaction entity) => entity.ToDto();
        protected override Transaction ToEntity(TransactionDTO dto) => dto.ToEntity();
        protected override void ApplyUpdate(TransactionDTO dto, Transaction entity) => dto.ApplyUpdate(entity);

        #endregion


        #region Model Specific Functions
        public async Task<TransactionDTO?> GetByTransactionId(string transactionId)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(a => a.TransactionId == transactionId);

            return transaction?.ToDto();
        }
        #endregion
    }
}