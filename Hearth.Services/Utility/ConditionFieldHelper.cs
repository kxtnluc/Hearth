using Hearth.Foundation.Attributes;
using Hearth.Foundation.Enums;

namespace Hearth.Services.Utility;

public record ConditionFieldInfo(string Label, string Path, E_FIELD_TYPE Type);

public static class ConditionFieldHelper
{
    /// <summary>
    /// Every enum member that has a [ConditionField] attribute, as label/path/type.
    /// </summary>
    public static List<ConditionFieldInfo> GetFields<TEnum>() where TEnum : struct, Enum
    {
        return Enum.GetValues<TEnum>()
            .Select(value => (Value: value, Meta: value.GetAttribute<ConditionFieldAttribute>()))
            .Where(x => x.Meta is not null)
            .Select(x => new ConditionFieldInfo(x.Value.ToDisplayString(), x.Meta!.Path, x.Meta.Type))
            .ToList();
    }
}