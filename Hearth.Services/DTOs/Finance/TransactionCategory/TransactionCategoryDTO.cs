using Hearth.Services.DTOs.Finance.Transaction;
using Hearth.Services.Interfaces;
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
        public bool Is_Need { get; set; }
        public string? Description { get; set; }
        public decimal? Weight { get; set; }
        public int? Example_Transaction_Id { get; set; }
        public string Hex_Color { get; set; } = "var(--color-info)"; // Pyre.Element/wwwroot/token.css
        public bool Ignore { get; set; } = false;
        public bool Income { get; set; } = false;
        #endregion
        // Joined Props
        public List<TransactionDTO>? Transactions { get; set; }
        public List<TransactionCategoryDTO> CategoryRules { get; set; } = default!;

        // Future Addition
        //[NotMapped]
        //public Transaction Example_Transaction { get; set; }
    }
}
