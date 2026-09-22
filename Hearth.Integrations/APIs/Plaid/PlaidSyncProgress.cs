using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid
{
    public class PlaidSyncProgress
    {
        public string Phase { get; set; } = string.Empty;
        public string Stage { get; set; } = string.Empty;
        public int CurrentBankIndex { get; set; }
        public string? CurrentInstitution { get; set; }
        public int TotalBanks { get; set; }
    }
}
