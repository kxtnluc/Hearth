using Hearth.Core.Models.Finance;
using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Services.DTOs.Finance.Bank;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Interfaces
{
    public interface IPlaidTransactionService
    {
        Task<PlaidTransactionSyncHttpResponse> SyncBankTransactions(string itemId, int recordsToSync = 50);
    }
}
