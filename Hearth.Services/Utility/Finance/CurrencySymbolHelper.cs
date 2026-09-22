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

        public static string ConvertDecimalToCurrency(decimal number, string currencyCode = "USD")
        {
            var format = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.InvariantCulture.NumberFormat.Clone();
            format.CurrencySymbol = GetCurrencySymbol(currencyCode);

            return number.ToString("C", format);
        }
    }
}
