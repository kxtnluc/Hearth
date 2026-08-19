using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.HttpResponses
{
    /// <summary>
    /// [transactions/sync] Response
    /// </summary>
    public class PlaidTransactionSyncHttpResponse
    {
        public List<PlaidAccountHttpResponse> Accounts { get; set; } = new();
        public List<PlaidTransactionHttpResponse> Added { get; set; } = new();
        public List<PlaidTransactionHttpResponse> Modified { get; set; } = new();
        public List<PlaidRemovedTransactionHttpResponse> Removed { get; set; } = new();
        public string Next_Cursor { get; set; } = default!;
        public bool Has_More { get; set; }
        public string Request_Id { get; set; } = default!;
        public string Transactions_Update_Status { get; set; } = default!;
    }

    /// <summary>
    /// A removed transaction only carries its identifiers, not the full transaction shape.
    /// </summary>
    public class PlaidRemovedTransactionHttpResponse
    {
        public string Account_Id { get; set; } = default!;
        public string Transaction_Id { get; set; } = default!;
    }
}
