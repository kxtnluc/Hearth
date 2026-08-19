using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Services.DTOs.Finance.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Interfaces
{
    public interface IPlaidAccountService
    {
        /// <summary>
        /// This one is used anytime after the bank has already been created, for updating purposes.
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        Task<List<AccountDTO>?> GetBankAccounts(string itemId);
        /// <summary>
        /// This one is used to get accounts from a new bank that was just linked in the StoreAccessToken function in IPlaidService.cs
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        Task<PlaidAccountGetHttpResponse> GetFreshBankAccountsWithItem(string accessToken);

    }
}
