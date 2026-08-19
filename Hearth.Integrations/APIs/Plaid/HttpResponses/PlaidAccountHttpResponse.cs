using Hearth.Services.DTOs.Finance.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Hearth.Integrations.APIs.Plaid.HttpResponses
{
    public class PlaidAccountHttpResponse
    {
        public string Account_Id { get; set; } = default!;
        public PlaidBalancesHttpResponse Balances { get; set; } = default!;
        public string? Mask { get; set; }
        public string? Name { get; set; }
        public string? Official_Name { get; set; }
        public string? Subtype { get; set; }
        public string Type { get; set; } = default!;
    }


    public class PlaidBalancesHttpResponse
    {
        public decimal? Available { get; set; }
        public decimal? Current { get; set; }
        public string? Iso_Currency_Code { get; set; }
        public decimal? Limit { get; set; }
        public string? Unofficial_Currency_Code { get; set; }
    }
}
