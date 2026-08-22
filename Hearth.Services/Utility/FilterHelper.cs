using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility
{
    public static class FilterHelper
    {
        public const string AnyOption = "any";

        public static string? NormalizeAnyOption(string? value) =>
            value == AnyOption ? null : value;
    }
}
