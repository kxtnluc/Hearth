using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid
{
    public class PlaidMajorSyncSummary
    {
        public PlaidTransactionSyncSummary Transaction { get; set; } = new PlaidTransactionSyncSummary();
        public PlaidAccountSyncSummary Account { get; set; } = new PlaidAccountSyncSummary();
    }

    public class PlaidTransactionSyncSummary
    {
        public int TotalAdded { get; set; }
        public int TotalModified { get; set; }
        public int TotalRemoved { get; set; }
    }

    public class PlaidAccountSyncSummary
    {
        public int TotalAdded { get; set; }
        public int TotalModified { get; set; }
        public int TotalRemoved { get; set; }

    }
}
