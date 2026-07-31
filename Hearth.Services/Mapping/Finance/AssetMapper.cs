using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Asset;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Mapping.Finance
{
    [Mapper]
    internal static partial class AssetMapper
    {
        /// <summary>
        /// Turns a Model (entity) into its DTO
        /// </summary>
        public static partial AssetDTO ToDto(this Asset entity);
        /// <summary>
        /// Turns a DTO back into its Model (entity)
        /// </summary>
        public static partial Asset ToEntity(this AssetDTO dto);
        /// <summary>
        /// Turns a Model List into its DTO List counterpart
        /// </summary>
        public static partial List<AssetDTO> ToDtoList(this List<Asset> entities);
        /// <summary>
        /// Applies non-null values from the DTO onto an existing tracked entity.
        /// Any property that's null on the DTO is left untouched on the entity.
        /// </summary>
        public static partial void ApplyUpdate(this AssetDTO dto, Asset entity);
    }
}
