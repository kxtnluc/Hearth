using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Bank;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces.Finance
{
    public interface IBankService : ISqliteTableService<BankDTO>
    {
        Task<BankDTO?> GetByItemId(string itemId);
    }
}
