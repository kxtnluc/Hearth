using Hearth.Core.Models;
using Hearth.Services.DTOs;
using Riok.Mapperly.Abstractions;

namespace Hearth.Services.Mapping;

//(AllowNullPropertyAssignment = false) might need this in the mapper tag
[Mapper]
internal static partial class RuleConditionMapper
{
    /// <summary>
    /// Turns a Model (entity) into its DTO
    /// </summary>
    public static partial RuleConditionDTO ToDto(this RuleCondition entity);
    /// <summary>
    /// Turns a DTO back into its Model (entity)
    /// </summary>
    public static partial RuleCondition ToEntity(this RuleConditionDTO dto);
    /// <summary>
    /// Turns a Model List into its DTO List counterpart
    /// </summary>
    public static partial List<RuleConditionDTO> ToDtoList(this List<RuleCondition> entities);
    /// <summary>
    /// Applies non-null values from the DTO onto an existing tracked entity.
    /// Any property that's null on the DTO is left untouched on the entity.
    /// </summary>
    public static partial void ApplyUpdate(this RuleConditionDTO dto, RuleCondition entity);
}