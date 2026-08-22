using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Hearth.Core.Models.Finance.ValueObjects
{
    public class AccountBalances
    {
        public decimal? Available { get; set; }

        public decimal Current { get; set; }

        public string Iso_Currency_Code { get; set; } = string.Empty;

        public decimal? Limit { get; set; }

        public string Unofficial_Currency_Code { get; set; } = string.Empty;
    }
}
