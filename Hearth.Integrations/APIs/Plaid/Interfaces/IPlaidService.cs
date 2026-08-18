using Hearth.Services.DTOs.Finance.Bank;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Interfaces
{
    public interface IPlaidService
    {
        Task<string?> CreateLinkTokenAsync(int userId);
        Task<BankDTO?> StoreAccessTokenAsync(string publicToken, int userId);
    }
}
