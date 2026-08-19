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
        protected override void ValidatePayload(AccountDTO payload)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions
        public async Task<AccountDTO?> GetByAccountId(string accountId)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Account_Id == accountId);

            return account?.ToDto();
        }
        #endregion
    }
}