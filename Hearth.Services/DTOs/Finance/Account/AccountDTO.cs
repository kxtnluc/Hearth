using Hearth.Services.Interfaces;
using Hearth.Services.Utility;
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
        /// <summary>
        /// User Set. Shows weather this account is open or closed.
        /// </summary>
        public bool IsOpen { get; set; } = true;
        /// <summary>
        /// The PLAID Item_Id of the bank this account is assosiated with. The user should not interfere with this.
        /// </summary>
        public string? Bank_Item_Id { get; set; }
        /// <summary>
        /// User Set. Gives the account a color. Effects banners and other things. Default is "Hearth Red"
        /// </summary>
        public string HexColor { get; set; } = "#ff6f4e";
        /// <summary>
        /// User Set.Not to be confused with Id, or Account_Id, this is the actual 12-digit account number the bank has assigned
        /// </summary>
        public string? AccountNumber { get; set; } = null;
        /// <summary>
        /// User Set. This name overwrites "Name" for display purposes.
        /// </summary>
        public string? HearthName { get; set; } = null;
        #endregion
        // DTO specific
        [MapperIgnore]
        public bool Is_Credit_C => Type?.Equals("credit", StringComparison.OrdinalIgnoreCase) ?? false;
        [MapperIgnore]
        public string HexColorText_C => ColorHelper.HexColorTextEvaluator(HexColor);
        /// <summary>
        /// A variable that evaluates to Name given by Plaid, unless a custom Name {HearthName} has been set.
        /// </summary>
        [MapperIgnore] 
        public string Name_C => !string.IsNullOrWhiteSpace(HearthName) ? HearthName : Name!;
        /// <summary>
        /// A set of strings to represent the bool {IsOpen} above
        /// </summary>
        [MapperIgnore]
        public string IsOpen_C => IsOpen ? "Open" : "Closed";


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
