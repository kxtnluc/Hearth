using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance
{
    public class BankCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal? Weight { get; set; }
        public string Hex_Color { get; set; } = "var(--info-color)";
        public bool Ignore { get; set; } = false;
        public bool Debit { get; set; } = false;
    }
}
