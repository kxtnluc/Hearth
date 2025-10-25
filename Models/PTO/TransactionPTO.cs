using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hearth.Models.PTO
{
    public class TransactionSyncResponsePTO
    {
        public List<TransactionPTO> Added { get; set; }
        public List<TransactionPTO> Modified { get; set; }
        public List<TransactionPTO> Removed { get; set; }
        public List<AccountPTO> Accounts { get; set; }
        public string Next_Cursor { get; set; }
        public bool Has_More { get; set; }
        public string Request_Id { get; set; }
        public string Transactions_Update_Status { get; set; }
    }

    public class TransactionPTO
    {
        public string Transaction_Id { get; set; }
        public string Account_Id { get; set; } // required
        public decimal Amount { get; set; } // required
        public string? Account_Owner { get; set; }
        public string? Authorized_Date { get; set; }
        public string? Authorized_Datetime { get; set; }
        public List<string>? Category { get; set; }
        public string Category_Id { get; set; }
        public string? Check_Number { get; set; }
        public List<object>? Counterparties { get; set; }
        public string Date { get; set; }
        public string? Datetime { get; set; }
        public string Iso_Currency_Code { get; set; }
        public Location? Location { get; set; }
        public string? Logo_Url { get; set; }
        public string? Merchant_Entity_Id { get; set; }
        public string? Merchant_Name { get; set; }
        public string Name { get; set; }
        public string? Payment_Channel { get; set; }
        public PaymentMeta? Payment_Meta { get; set; }
        public bool Pending { get; set; }
        public string? Pending_Transaction_Id { get; set; }
        public PersonalFinanceCategory? Personal_Finance_Category { get; set; }
        public string Personal_Finance_Category_Icon_Url { get; set; }
        public string? Transaction_Code { get; set; }
        public string? Transaction_Type { get; set; }
        public string? Unofficial_Currency_Code { get; set; }
        public string? Website { get; set; }
    }

    public class Location
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string? Postal_Code { get; set; }
        public string? Region { get; set; }
        public string? Store_Number { get; set; }
    }

    public class PaymentMeta
    {
        public string? By_Order_Of { get; set; }
        public string? Payee { get; set; }
        public string? Payer { get; set; }
        public string? Payment_Method { get; set; }
        public string? Payment_Processor { get; set; }
        public string? Ppd_Id { get; set; }
        public string? Reason { get; set; }
        public string? Reference_Number { get; set; }
    }

    public class PersonalFinanceCategory
    {
        public string? Confidence_Level { get; set; }
        public string? Detailed { get; set; }
        public string? Primary { get; set; }
    }
}
