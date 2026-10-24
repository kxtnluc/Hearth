using Hearth.Foundation.Enums;
using Hearth.Services.DTOs;

namespace Hearth.Services.Utility;

public static class RuleHelper
{
    /// <summary>
    /// True if the item passes this single condition.
    /// </summary>
    public static bool Evaluate<T>(RuleConditionDTO condition, T item)
    {
        if (condition is null || item is null) return false;

        var numericOp = EnumDisplayHelper.FromDisplayString<E_OPERATOR>(condition.Condition);
        var stringOp = EnumDisplayHelper.FromDisplayString<E_STRING_OPERATOR>(condition.Condition);
        var boolOp = EnumDisplayHelper.FromDisplayString<E_BOOLEAN_OPERATOR>(condition.Condition);
        if (numericOp is null && stringOp is null && boolOp is null) return false;

        // This is here because Enums cannot have a . in them, so when a enum needs to represent a nested field
        // I mark the nest with a __
        var _field = condition.Field.Replace("__", ".");

        string value = GetValueByPath(item, _field)?.ToString() ?? string.Empty;

        if (numericOp is not null) return EvaluateNumeric(numericOp.Value, value, condition.Match);
        else if (stringOp is not null) return EvaluateString(stringOp!.Value, value, condition.Match);
        else if (boolOp is not null) return EvaluateBool(boolOp!.Value, value, condition.Match);

        return false;
    }

    /// <summary>
    /// True if the item passes every condition. A rule with no conditions matches nothing.
    /// </summary>
    public static bool PassesAll<T>(IEnumerable<RuleConditionDTO>? conditions, T item)
    {
        var list = conditions?.ToList();
        if (list is null || list.Count == 0) return false;

        return list.All(c => Evaluate(c, item));
    }

    private static bool EvaluateNumeric(E_OPERATOR op, string value, string match)
    {
        // Trys to parse the value as a float, if it fails return false
        if (!float.TryParse(value, out var valueNum)) return false;

        if (op == E_OPERATOR.Between)
        {
            var matchRange = match.Split('-');
            if (matchRange.Length != 2) return false;
            if (!float.TryParse(matchRange[0], out var min)) return false;
            if (!float.TryParse(matchRange[1], out var max)) return false;

            return valueNum >= min && valueNum <= max;
        }

        if (!float.TryParse(match, out var matchNum)) return false;

        return op switch
        {
            E_OPERATOR.Equals => valueNum == matchNum,
            E_OPERATOR.GreaterThan => valueNum > matchNum,
            E_OPERATOR.LessThan => valueNum < matchNum,
            E_OPERATOR.GreaterThanOrEqualTo => valueNum >= matchNum,
            E_OPERATOR.LessThanOrEqualTo => valueNum <= matchNum,
            E_OPERATOR.NotEquals => valueNum != matchNum,
            _ => false,
        };
    }

    private static bool EvaluateString(E_STRING_OPERATOR op, string value, string match)
    {
        return op switch
        {
            E_STRING_OPERATOR.Exact => string.Equals(value, match, StringComparison.OrdinalIgnoreCase),
            E_STRING_OPERATOR.StartsWith => value.StartsWith(match, StringComparison.OrdinalIgnoreCase),
            E_STRING_OPERATOR.EndsWith => value.EndsWith(match, StringComparison.OrdinalIgnoreCase),
            E_STRING_OPERATOR.Contains => value.Contains(match, StringComparison.OrdinalIgnoreCase),
            E_STRING_OPERATOR.IsNotExact => !string.Equals(value, match, StringComparison.OrdinalIgnoreCase),
            _ => false,
        };
    }

    private static bool EvaluateBool(E_BOOLEAN_OPERATOR op, string value, string match)
    {
        bool _value = bool.Parse(value);
        bool _match = bool.Parse(match);

        return op switch
        {
            E_BOOLEAN_OPERATOR.Is => _value == _match,
            E_BOOLEAN_OPERATOR.IsNot => _value != _match,
            _ => false
        };
    }

    /// <summary>
    /// Reads a property by name, following dots into nested objects
    /// (e.g. "Personal_Finance_Category.Primary"). Returns null if any step is missing.
    /// </summary>
    private static object? GetValueByPath(object obj, string path)
    {
        object? current = obj;

        foreach (var part in path.Split('.'))
        {
            if (current is null) return null;

            var prop = current.GetType().GetProperty(part);
            if (prop is null) return null;

            current = prop.GetValue(current);
        }

        return current;
    }
}