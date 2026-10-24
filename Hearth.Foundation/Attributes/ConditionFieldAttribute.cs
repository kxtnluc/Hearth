using Hearth.Foundation.Enums;

namespace Hearth.Foundation.Attributes;

/// <summary>
/// Metadata for a field a rule condition can target: the property path on the DTO,
/// and what kind of value lives there.
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public sealed class ConditionFieldAttribute : Attribute
{
    public string Path { get; }
    public E_FIELD_TYPE Type { get; }

    public ConditionFieldAttribute(string path, E_FIELD_TYPE type)
    {
        Path = path;
        Type = type;
    }
}