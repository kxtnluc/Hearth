using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance.ValueObjects
{
    public class TransactionPersonalFinanceCategory
    {
        public string? Primary { get; set; }
        public string? Detailed { get; set; }
        public string? Confidence_Level { get; set; }
    }
}
