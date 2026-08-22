using Hearth.Services.Interfaces;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs.Finance.Account
{
    public class AccountDTO : IDTO
    {
        public int Id { get; set; }
        #region Plaid
        public string Account_Id { get; set; } = default!;
        public BalancesDTO? Balances { get; set; }
        public string? Mask { get; set; }
        public string? Name { get; set; }
        public string? Official_Name { get; set; }
        public string? Type { get; set; }
        public string? Subtype { get; set; }
        #endregion
        #region Hearth
        public bool? IsOpen { get; set; }
        public string? Bank_Item_Id { get; set; }

        #endregion
        // DTO specific
        [MapperIgnore]
        public bool Is_Credit_C => Type?.Equals("credit", StringComparison.OrdinalIgnoreCase) ?? false;


        // public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }

    public class BalancesDTO
    {
        public decimal? Available { get; set; }

        public decimal? Current { get; set; }

        public string? Iso_Currency_Code { get; set; } = string.Empty;

        public decimal? Limit { get; set; }

        public string? Unofficial_Currency_Code { get; set; } = string.Empty;
    }
}
