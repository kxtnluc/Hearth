using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Core.Models.Finance
{
    public class TransactionCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public decimal? Weight { get; set; }
        public string HexColor { get; set; } = "var(--info-color)";
        public string? IconName { get; set; } = null;
        public bool IsNeed { get; set; }
        public bool Ignore { get; set; } = false;
        public List<TransactionCategoryRule> Rules { get; set; } = new();
    }
}
