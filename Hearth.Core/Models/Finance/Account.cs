// PLAID API
using Microsoft.EntityFrameworkCore.Metadata;
using Hearth.Core.Interfaces;
using Hearth.Core.Models.Finance.ValueObjects;

namespace Hearth.Core.Models.Finance
{
    public class Account : ISqliteTable
    {
        public int Id { get; set; }
        #region Plaid
        public string Account_Id { get; set; } = default!;
        public AccountBalances? Balances { get; set; }
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
        /// User Set. Not to be confused with Id, or Account_Id, this is the actual 12-digit account number the bank has assigned
        /// </summary>
        public string? AccountNumber { get; set; } = null;
        /// <summary>
        /// User Set. The bank account's routing number
        /// </summary>
        public string? AccountRoutingNumber { get; set; } = null;
        /// <summary>
        /// User Set. This name overwrites "Name" for display purposes.
        /// </summary>
        public string? HearthName { get; set; } = null;
        #endregion
    }
}