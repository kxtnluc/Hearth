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
        public string AccountId { get; set; } = default!;
        public string BankId { get; set; } = default!;
        //TODO: Move to DTOs 
        //[ForeignKey("BankId")]
        //public Bank Bank { get; set; }
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
        // DTO specific
        [MapperIgnore]
        public bool Is_Credit_C => Type?.Equals("credit", StringComparison.OrdinalIgnoreCase) ?? false;


        // public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
