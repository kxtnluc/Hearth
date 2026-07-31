using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Microsoft.EntityFrameworkCore;

namespace Hearth.Services.Services.Finance
{
    public class AccountService : ASqliteTableService<Account, AccountDTO>, IAccountService
    {
        public AccountService(HearthDbContext context) : base(context) { }
        #region Abstract Class Setup
        protected override DbSet<Account> DbSet => _context.Accounts;
        protected override AccountDTO ToDto(Account entity) => entity.ToDto();
        protected override Account ToEntity(AccountDTO dto) => dto.ToEntity();
        protected override void ApplyUpdate(AccountDTO dto, Account entity) => dto.ApplyUpdate(entity);
        #endregion

        #region Model Specific Functions
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