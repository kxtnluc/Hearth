using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.HttpResponses
{
    public class PlaidTransactionHttpResponse
    {
        public string Transaction_Id { get; set; } = default!;
        public string Account_Id { get; set; } = default!;
        public decimal Amount { get; set; }
        public string? Account_Owner { get; set; }
        public string? Authorized_Date { get; set; }
        public string? Authorized_Datetime { get; set; }
        public string? Check_Number { get; set; }
        public List<PlaidCounterpartyHttpResponse>? Counterparties { get; set; }
        public string Date { get; set; } = default!;
        public string? Datetime { get; set; }
        public string Iso_Currency_Code { get; set; } = default!;
        public string? Unofficial_Currency_Code { get; set; }
        public PlaidLocationHttpResponse? Location { get; set; }
        public string? Logo_Url { get; set; }
        public string? Merchant_Entity_Id { get; set; }
        public string? Merchant_Name { get; set; }
        public string? Merchant_Category_Code { get; set; }
        public string Name { get; set; } = default!;
        public string? Payment_Channel { get; set; }
        public PlaidPaymentMetaHttpResponse? Payment_Meta { get; set; }
        public bool Pending { get; set; }
        public string? Pending_Transaction_Id { get; set; }
        public PlaidPersonalFinanceCategoryHttpResponse? Personal_Finance_Category { get; set; }
        public string? Personal_Finance_Category_Icon_Url { get; set; }
        public string? Transaction_Code { get; set; }
        public string? Transaction_Type { get; set; }
    }

    public class PlaidCounterpartyHttpResponse
    {
        public string? Name { get; set; }
        public string? Type { get; set; }
        public string? Logo_Url { get; set; }
        public string? Website { get; set; }
        public string? Entity_Id { get; set; }
        public string? Confidence_Level { get; set; }
    }

    public class PlaidLocationHttpResponse
    {
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? Postal_Code { get; set; }
        public string? Country { get; set; }
        public double? Lat { get; set; }
        public double? Lon { get; set; }
        public string? Store_Number { get; set; }
    }
    public class PlaidPaymentMetaHttpResponse
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

    public class PlaidPersonalFinanceCategoryHttpResponse
    {
        public string? Primary { get; set; }
        public string? Detailed { get; set; }
        public string? Confidence_Level { get; set; }
    }
}
