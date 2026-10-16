using Hearth.Services.CTOs.Finance.Account;
using Hearth.Services.DTOs;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.Filters.Finance;

namespace Hearth.Services.Interfaces.Finance
{
    public interface IAccountService : ISqliteTableService<AccountDTO, AccountFilter>
    {
        Task<AccountDTO?> GetByAccountId(string accountId);
        Task<List<AccountDTO>?> GetByUserId(int userId);
        Task<AccountCTO?> GenerateAccountCTO(AccountDTO account, AccountCTO_Options options);
        Task<AccountDTO?> GetByIdWithTransactions(int id);
        Task<List<AccountDTO>?> GetRangeWithAll(int[]? accountIds = null, bool track = false);
    }
}
