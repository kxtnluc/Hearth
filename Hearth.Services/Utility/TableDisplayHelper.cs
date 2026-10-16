using Hearth.Services.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Hearth.Services.Utility
{
    public static class TableDisplayHelper
    {
        /// <summary>
        /// Formats a property's value for display: lists are joined into a comma-separated
        /// string, nested DTOs (under Hearth.Services.DTOs) are flattened one level deep,
        /// and null values render as an em dash.
        /// </summary>
        public static string FormatPropertyValue(PropertyInfo prop, object obj)
        {
            var value = prop.GetValue(obj);

            if (value is null) return "—";
            if (value is string s) return s;

            if (value is IEnumerable enumerable)
            {
                var items = enumerable.Cast<object>().Select(i => i?.ToString() ?? "null");
                return string.Join(", ", items);
            }

            var type = value.GetType();
            if (type.Namespace?.StartsWith("Hearth.Services.DTOs") == true)
            {
                var nestedProps = type.GetProperties()
                    .Select(p => $"{p.Name}: {p.GetValue(value) ?? "—"}");
                return string.Join(" | ", nestedProps);
            }

            return value.ToString() ?? "—";
        }

        /// <summary>
        /// Returns every public property on the given object as (PropertyInfo, formatted value) pairs,
        /// ready to render as a label/value list.
        /// </summary>
        public static IEnumerable<(PropertyInfo Property, string DisplayValue)> GetDisplayableProperties(object obj)
        {
            return obj.GetType()
                .GetProperties()
                .Select(p => (p, FormatPropertyValue(p, obj)));
        }


        /// <summary>
        /// Returns the Id of every DTO in the list.
        /// </summary>
        public static int[] GetDtoIds<T>(this IEnumerable<T> items) where T : IDTO
        {
            return items.Select(i => i.Id).ToArray();
        }
    }
}
