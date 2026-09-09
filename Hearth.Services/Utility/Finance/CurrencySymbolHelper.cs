using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility.Finance
{
    public static class CurrencySymbolHelper
    {
        public static string GetCurrencySymbol(string currencyCode) => currencyCode switch
        {
            "USD" => "$",
            "EUR" => "€",
            "GBP" => "£",
            "JPY" => "¥",
            "CAD" => "$",
            _ => currencyCode + " "
        };
    }
}
