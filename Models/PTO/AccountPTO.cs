using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Hearth.Models.PTO
{
    public class AccountResponsePTO
    {
        [JsonPropertyName("accounts")]
        public List<AccountPTO> Accounts { get; set; }

        [JsonPropertyName("item")]
        public PlaidItemPTO Item { get; set; }

        [JsonPropertyName("request_id")]
        public string RequestId { get; set; }
    }
    public class AccountPTO
    {
        [JsonPropertyName("account_id")]
        public string AccountId { get; set; }

        [JsonPropertyName("balances")]
        public BalancesPTO Balances { get; set; }

        [JsonPropertyName("mask")]
        public string Mask { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("official_name")]
        public string OfficialName { get; set; }

        [JsonPropertyName("subtype")]
        public string Subtype { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

    public class BalancesPTO
    {
        [JsonPropertyName("available")]
        public decimal? Available { get; set; }

        [JsonPropertyName("current")]
        public decimal Current { get; set; }

        [JsonPropertyName("iso_currency_code")]
        public string IsoCurrencyCode { get; set; }

        [JsonPropertyName("limit")]
        public decimal? Limit { get; set; }

        [JsonPropertyName("unofficial_currency_code")]
        public string UnofficialCurrencyCode { get; set; }
    }

    public class PlaidItemPTO
    {
        [JsonPropertyName("institution_id")]
        public string InstitutionId { get; set; }

        [JsonPropertyName("institution_name")]
        public string InstitutionName { get; set; }
        [JsonPropertyName("item_id")]
        public string ItemId { get; set; }
    }
}
