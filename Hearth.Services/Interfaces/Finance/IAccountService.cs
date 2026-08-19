using Hearth.Services.DTOs.Finance.Account;

namespace Hearth.Services.Interfaces.Finance
{
    public interface IAccountService : ISqliteTableService<AccountDTO>
    {
        Task<AccountDTO?> GetByAccountId(string accountId);
    }
}
