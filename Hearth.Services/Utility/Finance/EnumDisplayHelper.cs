using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Hearth.Services.Utility.Finance
{
    public static class EnumDisplayHelper
    {
        public static string ToDisplayString<TEnum>(this TEnum value) where TEnum : struct, Enum
        {
            var field = typeof(TEnum).GetField(value.ToString());
            var attr = field?.GetCustomAttribute<DescriptionAttribute>();
            return attr?.Description ?? value.ToString();
        }

        public static TEnum? FromDisplayString<TEnum>(string input) where TEnum : struct, Enum
        {
            foreach (var value in Enum.GetValues<TEnum>())
            {
                if (value.ToDisplayString() == input)
                    return value;
            }
            return null;
        }
    }
}
