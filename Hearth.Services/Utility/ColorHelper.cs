using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Utility
{
    public static class ColorHelper
    {
        public static string HexColorTextEvaluator(string backgroundHexColor)
        {
            // Strip # if present
            var hex = backgroundHexColor.TrimStart('#');

            // Parse RGB components
            var r = Convert.ToInt32(hex.Substring(0, 2), 16);
            var g = Convert.ToInt32(hex.Substring(2, 2), 16);
            var b = Convert.ToInt32(hex.Substring(4, 2), 16);

            // Calculate relative luminance using WCAG formula
            var luminance = (0.299 * r + 0.587 * g + 0.114 * b) / 255;

            // Above 0.5 = light background = needs black text
            if (luminance > 0.5) return "#000000";
            else return "#ffffff";
        }
    }
}
