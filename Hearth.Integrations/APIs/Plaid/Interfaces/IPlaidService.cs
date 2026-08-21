using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Services.DTOs.Finance.Bank;
using Hearth.Services.DTOs.Finance.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Interfaces
{
    public interface IPlaidService
    {
        Task<string?> CreateLinkTokenAsync(int userId);
        Task<BankDTO?> StoreAccessTokenAsync(string publicToken, int userId);
        Task MajorSync(PlaidSyncOptions? plaidSyncOptions = null);
    }
}
