using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid
{
    public class PlaidMajorSyncOptions
    {
        public bool SyncTransactions { get; set; } = true;
        public bool SyncAccounts { get; set; } = true;
        public int SyncPastDays { get; set; } = 50;
        public int NumberOfTransactionToSync { get; set; } = 50;
    }
}
