// PLAID API
using Hearth.Core.Interfaces;
using Hearth.Core.Models.Finance.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Hearth.Core.Models.Finance
{
    public class Transaction : ISqliteTable
    {
        public int Id { get; set; }
        public string Transaction_Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Date { get; set; } = default!;
        public decimal Amount { get; set; }
        public string Account_Id { get; set; } = default!;
        public string? Account_Owner { get; set; }
        public string? Authorized_Date { get; set; }
        public string? Authorized_Datetime { get; set; }
        public int? CategoryId { get; set; }
        public string? Check_Number { get; set; }
        public string? Datetime { get; set; }
        public string Iso_Currency_Code { get; set; } = default!;
        public string? Logo_Url { get; set; }
        public string? Merchant_Entity_Id { get; set; }
        public string? Merchant_Name { get; set; }
        public string? Merchant_Category_Code { get; set; }
        public string? Payment_Channel { get; set; }
        public bool Pending { get; set; }
        public string? Pending_Transaction_Id { get; set; }
        public string Personal_Finance_Category_Icon_Url { get; set; } = default!;
        public string? Transaction_Code { get; set; }
        public string? Transaction_Type { get; set; }
        public string? Unofficial_Currency_Code { get; set; }
        public List<TransactionCounterparty> Counterparties { get; set; } = new();
        public TransactionLocation? Location { get; set; }
        public TransactionPaymentMeta? Payment_Meta { get; set; }
        public TransactionPersonalFinanceCategory? Personal_Finance_Category { get; set; }
    }
}