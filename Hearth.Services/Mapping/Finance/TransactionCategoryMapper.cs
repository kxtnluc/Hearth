using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.TransactionCategory;
using Riok.Mapperly.Abstractions;

namespace Hearth.Services.Mapping.Finance;

//(AllowNullPropertyAssignment = false) might need this in the mapper tag
[Mapper]
internal static partial class TransactionCategoryMapper
{
    /// <summary>
    /// Turns a Model (entity) into its DTO
    /// </summary>
    public static partial TransactionCategoryDTO ToDto(this TransactionCategory entity);
    /// <summary>
    /// Turns a DTO back into its Model (entity)
    /// </summary>
    public static partial TransactionCategory ToEntity(this TransactionCategoryDTO dto);
    /// <summary>
    /// Turns a Model List into its DTO List counterpart
    /// </summary>
    public static partial List<TransactionCategoryDTO> ToDtoList(this List<TransactionCategory> entities);
    /// <summary>
    /// Applies non-null values from the DTO onto an existing tracked entity.
    /// Any property that's null on the DTO is left untouched on the entity.
    /// </summary>
    public static partial void ApplyUpdate(this TransactionCategoryDTO dto, TransactionCategory entity);
    /// <summary>
    /// Deep-copies every property from source onto target — used to implement Clone()
    /// without Mapperly's same-type identity-mapping shortcut kicking in.
    /// </summary>
    public static partial void CopyInto(this TransactionCategoryDTO source, TransactionCategoryDTO target);
}
