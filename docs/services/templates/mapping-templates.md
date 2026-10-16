# Mapping Template (Finance)

### Full Template

```csharp
using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.__object;
using Riok.Mapperly.Abstractions;

namespace Hearth.Services.Mapping.Finance;

//(AllowNullPropertyAssignment = false) might need this in the mapper tag
[Mapper]
internal static partial class __objectMapper
{
    /// <summary>
    /// Turns a Model (entity) into its DTO
    /// </summary>
    public static partial __objectDTO ToDto(this __object entity);
    /// <summary>
    /// Turns a DTO back into its Model (entity)
    /// </summary>
    public static partial __object ToEntity(this __objectDTO dto);
    /// <summary>
    /// Turns a Model List into its DTO List counterpart
    /// </summary>
    public static partial List<__objectDTO> ToDtoList(this List<__object> entities);
    /// <summary>
    /// Applies non-null values from the DTO onto an existing tracked entity.
    /// Any property that's null on the DTO is left untouched on the entity.
    /// </summary>
    public static partial void ApplyUpdate(this __objectDTO dto, __object entity);
    /// <summary>
    /// Deep-copies every property from source onto target — used to implement Clone()
    /// without Mapperly's same-type identity-mapping shortcut kicking in.
    /// </summary>
    public static partial void CopyInto(this __objectDTO source, __objectDTO target);
}
```

## Placeholders to Replace


## Structure Notes

