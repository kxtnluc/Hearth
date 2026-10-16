using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;

namespace Hearth.Services.Utility
{
    public static class EnumDisplayHelper
    {
        /// <summary>
        /// Takes in an Enum, spits out its description string
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
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

        public static string[] ToDisplayStringArray<TEnum>(bool withAnyOption = false) where TEnum : struct, Enum
        {
            var result = Enum.GetValues<TEnum>()
                .Select(value => value.ToDisplayString());

            if (withAnyOption) result = result.Prepend("any");

            return result.ToArray();
        }
        /// <summary>
        /// Converts a string description on the "Source" ENUM, usually eStringOperator, into its "Target" enum description, usually E_CONDITION
        /// EnumDisplayHelper.ConvertDisplayString<E_STRING_OPERATOR, E_CONDITION>("starts-with");
        /// "starts-with" → "Starts With"
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="sourceDisplay"></param>
        /// <returns></returns>
        public static string? ConvertDisplayString<TSource, TTarget>(string sourceDisplay)
            where TSource : struct, Enum
            where TTarget : struct, Enum
        {
            // Find the source enum value that matches the input display string
            var sourceValue = FromDisplayString<TSource>(sourceDisplay);
            if (sourceValue == null) return null;

            // Get the name of that enum value (e.g. "StartsWith")
            var sourceName = sourceValue.Value.ToString();

            // Find a matching enum value in TTarget with the same name
            if (Enum.TryParse<TTarget>(sourceName, out var targetValue))
            {
                return targetValue.ToDisplayString();
            }

            return null;
        }
    }
}
