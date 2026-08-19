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
        public bool? IsOpen { get; set; }
        public string? Bank_Item_Id { get; set; }
        #endregion
    }
}