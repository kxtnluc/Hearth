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
        public bool IsNeed { get; set; }
        public string? Description { get; set; }
        public decimal? Weight { get; set; }
        public string HexColor { get; set; } = "var(--color-info-faded)"; // Pyre.Element/wwwroot/token.css
        public bool Ignore { get; set; } = false;
        public bool Income { get; set; } = false;
        #endregion
        [MapperIgnore]
        public string HexColorText_C => ColorHelper.HexColorTextEvaluator(HexColor);
        // Joined Props
        //public List<TransactionDTO>? Transactions { get; set; }
        //public List<TransactionCategoryDTO> CategoryRules { get; set; } = default!;

        // Future Addition
        //[NotMapped]
        //public Transaction Example_Transaction { get; set; }
    }
}
