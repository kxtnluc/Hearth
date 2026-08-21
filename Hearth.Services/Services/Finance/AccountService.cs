using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.Utility;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Microsoft.EntityFrameworkCore;
using Hearth.Services.Filters.Finance;

namespace Hearth.Services.Services.Finance
{
    public class AccountService : ASqliteTableService<Account, AccountDTO, AccountFilter>, IAccountService
    {
        private readonly IBankService _bankService;

        public AccountService(
            HearthDbContext context,
            IBankService bankService
        )
        : base(context)
        {
            _bankService = bankService;
        }
        #region Abstract Class Setup
        protected override DbSet<Account> DbSet => _context.Accounts;
        protected override AccountDTO ToDto(Account entity) => entity.ToDto();
        protected override Account ToEntity(AccountDTO dto) => dto.ToEntity();
        protected override List<AccountDTO> ToDtoList(List<Account> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(AccountDTO dto, Account entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(AccountDTO payload)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Filter
        public override List<AccountDTO> Filter(List<AccountDTO> accounts, AccountFilter filter)
        {
            var blankQuery = accounts.AsQueryable();

            var query = AccountFilter.BuildQuery(blankQuery, filter);

            var results = query.ToList();
            return results;
        }
        #endregion

        #region Model Specific Functions
        public async Task<AccountDTO?> GetByAccountId(string accountId)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Account_Id == accountId);

            return account?.ToDto();
        }

        public async Task<List<AccountDTO>?> GetByUserId(int userId)
        {
            var bankItemIds = await _bankService.GetItemIdsByUserId(userId);

            var userAccounts = await _context.Accounts
                .Where(a => bankItemIds.Contains(a.Bank_Item_Id!))
                .ToListAsync();

            if (userAccounts == null || userAccounts.Count == 0)
            {
                throw new HearthRecordNotFoundException("No accounts found under user: " + userId);
            }

            return ToDtoList(userAccounts);
        }
        #endregion
    }
}