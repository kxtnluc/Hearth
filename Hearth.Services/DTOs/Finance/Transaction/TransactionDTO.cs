using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.DTOs.Finance.Transaction
{
    public class TransactionDTO
    {
        #region Generic Props
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        #endregion
        #region Model Specific Props
        public int? CategoryId { get; set; }
        #endregion
        #region Plaid Mapped Props
        public string TransactionId { get; set; } = default!;
        public string Date { get; set; } = default!;
        public decimal Amount { get; set; }
        public string AccountId { get; set; } = default!; // |
        public string? Account_Owner { get; set; }
        public string? Authorized_Date { get; set; }
        public string? Authorized_Datetime { get; set; }
        // please work for the love of god
        //public Category? Category { get; set; }
        public string? Plaid_Category_Id { get; set; }
        public string? Check_Number { get; set; }
        public string? Datetime { get; set; }
        public string Iso_Currency_Code { get; set; } = default!;
        public string? Logo_Url { get; set; }
        public string? Merchant_Entity_Id { get; set; }
        public string? Merchant_Name { get; set; }
        public string? Payment_Channel { get; set; }
        public bool Pending { get; set; }
        public string? Pending_Transaction_Id { get; set; }
        public string Personal_Finance_Category_Icon_Url { get; set; } = default!;
        public string? Transaction_Code { get; set; }
        public string? Transaction_Type { get; set; }
        public string? Unofficial_Currency_Code { get; set; }
        public string? Personal_Finance_Category_Primary { get; set; }
        public string? Personal_Finance_Category_Detailed { get; set; }
        public string? Personal_Finance_Category_Confidence_Level { get; set; }
        // CounterPart Variables
        public string Confidence_Level { get; set; } = default!;
        public string EntityId { get; set; } = default!;
        public string LogoUrl { get; set; } = default!;
        public string Counter_Party_Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Type { get; set; } = default!;
        public string Website { get; set; } = default!;
        // Location Variables
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Country { get; set; } = default!;
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string Postal_Code { get; set; } = default!;
        public string Region { get; set; } = default!;
        public string Store_Number { get; set; } = default!;
        // PaymentMeta Variables
        public string By_Order_Of { get; set; } = default!;
        public string Payee { get; set; } = default!;
        public string Payer { get; set; } = default!;
        public string Payment_Method { get; set; } = default!;
        public string Payment_Processor { get; set; } = default!;
        public string PpdId { get; set; } = default!;
        public string Reason { get; set; } = default!;
        public string Reference_Number { get; set; } = default!;
        #endregion
    }
}
