using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance.ValueObjects
{
    public class TransactionCounterparty
    {
        public int Id { get; set; } // required for OwnsMany — EF needs a key per row
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Logo_Url { get; set; }
        public string? Website { get; set; }
        public string? Entity_Id { get; set; }
        public string? Confidence_Level { get; set; }
    }
}
