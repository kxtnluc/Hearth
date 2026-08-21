using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid
{
    public class PlaidSyncProgress
    {
        public int CurrentBankIndex { get; set; }
        public int TotalBanks { get; set; }
        public string? CurrentInstitution { get; set; }
        public string Stage { get; set; } = string.Empty;
    }

    public class PlaidSyncSummary
    {
        public int TotalAdded { get; set; }
        public int TotalModified { get; set; }
        public int TotalRemoved { get; set; }
    }
}
