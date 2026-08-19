using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance.ValueObjects
{
    public class TransactionLocation
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string? Postal_Code { get; set; }
        public string? Region { get; set; }
        public string? Store_Number { get; set; }
    }
}
