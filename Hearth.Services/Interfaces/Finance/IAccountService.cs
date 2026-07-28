using Hearth.Services.DTOs.Finance.Account;

namespace Hearth.Services.Interfaces.Finance
{
    public interface IAccountService : ISqliteTableService<AccountDTO>
    {
        Task<AccountDTO?> GetByAccountNumber(string accountNumber);

        Task<List<AccountDTO>> GetByBankId(string bankId);
    }
}
