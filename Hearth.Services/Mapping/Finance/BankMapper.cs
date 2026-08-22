using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Bank;
using Riok.Mapperly.Abstractions;
using System.Security.AccessControl;
namespace Hearth.Services.Mapping.Finance;

[Mapper]
internal static partial class BankMapper
{
    /// <summary>
    /// Turns a Model (entity) into its DTO
    /// </summary>
    public static partial BankDTO ToDto(this Bank entity);

    /// <summary>
    /// Turns a DTO back into its Model (entity)
    /// </summary>
    public static partial Bank ToEntity(this BankDTO dto);

    /// <summary>
    /// Turns a Model List into its DTO List counterpart
    /// </summary>
    public static partial List<BankDTO> ToDtoList(this List<Bank> entities);

    /// <summary>
    /// Applies non-null values from the DTO onto an existing tracked entity.
    /// Any property that's null on the DTO is left untouched on the entity.
    /// </summary>
    public static partial void ApplyUpdate(this BankDTO dto, Bank entity);
}