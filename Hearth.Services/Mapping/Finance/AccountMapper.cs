using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Account;
using Riok.Mapperly.Abstractions;

namespace Hearth.Services.Mapping.Finance;

//(AllowNullPropertyAssignment = false) might need this in the mapper tag
[Mapper]
internal static partial class AccountMapper
{
    /// <summary>
    /// Turns a Model (entity) into its DTO
    /// </summary>
    public static partial AccountDTO ToDto(this Account entity);
    /// <summary>
    /// Turns a DTO back into its Model (entity)
    /// </summary>
    public static partial Account ToEntity(this AccountDTO dto);
    /// <summary>
    /// Turns a Model List into its DTO List counterpart
    /// </summary>
    public static partial List<AccountDTO> ToDtoList(this List<Account> entities);
    /// <summary>
    /// Applies non-null values from the DTO onto an existing tracked entity.
    /// Any property that's null on the DTO is left untouched on the entity.
    /// </summary>
    public static partial void ApplyUpdate(this AccountDTO dto, Account entity);
    /// <summary>
    /// Deep-copies every property from source onto target — used to implement Clone()
    /// without Mapperly's same-type identity-mapping shortcut kicking in.
    /// </summary>
    public static partial void CopyInto(this AccountDTO source, AccountDTO target);
}