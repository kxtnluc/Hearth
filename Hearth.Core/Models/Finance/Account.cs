using Microsoft.EntityFrameworkCore.Metadata;
using Hearth.Core.Interfaces;

namespace Hearth.Core.Models.Finance
{
    public class Account : ISqliteTable
    {
        public int Id { get; set; }
        public string AccountId { get; set; } = default!;
        public string BankId { get; set; } = default!;
        public string? Mask { get; set; }
        public string? Name { get; set; }
        public string? Offical_Name { get; set; }
        public string? Type { get; set; }
        public string? Subtype { get; set; }
        public int UserId { get; set; }
        public string? Institution_Id { get; set; }
        public string? Institution_Name { get; set; }
        public string? Item_Id { get; set; }
        public string? Request_Id { get; set; }
        public DateTime? Inital_Date_Requested { get; set; }
        public DateTime? Last_Date_Requested { get; set; }
        public DateTime? Last_Modified { get; set; }
        public decimal? Balance_Available { get; set; } // The Remainig credit on Credit accounts
        public decimal? Balance_Current { get; set; } // The Balance on Debit accounts & The amount spent on Credit accounts.
        public string? Account_Number { get; set; }
        public bool? Is_Open { get; set; }
    }
}