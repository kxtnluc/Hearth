using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Interfaces;
using Hearth.Services.Utility;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs.Finance.TransactionCategory
{
    public class TransactionCategoryDTO : IDTO
    {
        #region Generic Props
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        #endregion
        #region Model Specific Props
        public string? Description { get; set; }
        public decimal? Weight { get; set; }
        public string HexColor { get; set; } = "var(--color-info-faded-2)"; // Pyre.Element/wwwroot/token.css
        public string? IconName { get; set; } = null; // In case i want to add assignable icons later on.
        public bool IsNeed { get; set; }
        public bool Ignore { get; set; } = false;
        #endregion
        // Joined Props
        public List<TransactionCategoryRuleDTO>? Rules { get; set; }
        [MapperIgnore]
        public string HexColorText_C => ColorHelper.HexColorTextEvaluator(HexColor);
    }
}
