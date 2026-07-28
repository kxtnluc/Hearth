using Hearth.Core.Data;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hearth.Services.Services.Finance
{
    public class AccountService : IAccountService
    {
        #region ISqliteTableService Functions
        
        private readonly HearthDbContext _context;

        public AccountService(HearthDbContext context)
        {
            _context = context;
        }

        public async Task<AccountDTO?> GetById(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            return account?.ToDto();
        }

        public async Task<List<AccountDTO>> GetAll()
        {
            var accounts = await _context.Accounts.AsNoTracking().ToListAsync();
            return accounts.ToDtoList();
        }

        public async Task<AccountDTO> Create(AccountDTO payload)
        {
            var entity = payload.ToEntity();
            _context.Accounts.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToDto();
        }

        public async Task Update(AccountDTO payload)
        {
            var entity = await _context.Accounts.FindAsync(payload.Id)
                ?? throw new KeyNotFoundException($"Account {payload.Id} not found");

            entity.Name = payload.Name;
            entity.Balance_Current = payload.Balance_Current;
            entity.BankId = payload.BankId;

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _context.Accounts.FindAsync(id);
            if (entity is null) return;

            _context.Accounts.Remove(entity);
            await _context.SaveChangesAsync();
        }
        #endregion
        #region IAccountService Functions
        public async Task<List<AccountDTO>> GetByBankId(string bankId)
        {
            var accounts = await _context.Accounts
                .AsNoTracking()
                .Where(a => a.BankId == bankId)
                .ToListAsync();

            return accounts.ToDtoList();
        }

        public async Task<AccountDTO?> GetByAccountNumber(string accountNumber)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Account_Number == accountNumber);

            return account?.ToDto();
        }
        #endregion
    }
}