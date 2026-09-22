using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility
{
    public static class FilterHelper
    {
        public const string AnyOption = "any";
        /// <summary>
        /// Turns a string of "any" into null, otherwise keeps the strings value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? NormalizeAnyOption(string? value) =>
            value == AnyOption ? null : value;
    }
}
