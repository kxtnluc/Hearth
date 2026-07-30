using Hearth.Core.Data;
using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Services.Finance
{
    public class TransactionService : ITransactionService
    {
        #region ISqliteTableService Functions

        private readonly HearthDbContext _context;

        public TransactionService(HearthDbContext context)
        {
            _context = context;
        }

        public async Task<TransactionDTO?> GetById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            return transaction?.ToDto();
        }

        public async Task<List<TransactionDTO>> GetAll()
        {
            var transactions = await _context.Transactions.AsNoTracking().ToListAsync();
            return transactions.ToDtoList();
        }

        public async Task<TransactionDTO> Create(TransactionDTO payload)
        {
            var entity = payload.ToEntity();
            _context.Transactions.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task Update(TransactionDTO payload)
        {
            var entity = await _context.Transactions.FindAsync(payload.Id)
                ?? throw new KeyNotFoundException($"Transaction {payload.Id} not found");

            entity.Name = payload.Name;
            // TODO
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Transactions.FindAsync(id);
            if (entity is null) return;

            _context.Transactions.Remove(entity);
            await _context.SaveChangesAsync();
        }
        #endregion
        #region ITransactionService Functions
        public async Task<TransactionDTO?> GetByTransactionId(string transactionId)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(a => a.TransactionId == transactionId);

            return transaction?.ToDto();
        }
        #endregion
    }
}
