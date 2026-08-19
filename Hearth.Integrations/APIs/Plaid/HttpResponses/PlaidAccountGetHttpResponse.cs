using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.HttpResponses
{
    // [accounts/get]
    public class PlaidAccountGetHttpResponse
    {
        public List<PlaidAccountHttpResponse>? Accounts { get; set; }

        public PlaidItemHttpResponse? Item { get; set; }

        public string Request_Id { get; set; } = default!;
    }

    public class PlaidItemHttpResponse
    {
        public string Institution_Id { get; set; }
        public string Institution_Name { get; set; }
        public string Item_Id { get; set; }
        public string Update_Type { get; set; }
        public string Webhook { get; set; }
        public string Auth_Method { get; set; }
        public string[] Available_Products { get; set; }
        public string[] Billed_Products { get; set; }
    }
}
