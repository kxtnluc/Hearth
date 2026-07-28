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
        public static partial AssetDTO ToDto(this Asset entity);
        public static partial Asset ToEntity(this AssetDTO dto);

        public static partial List<AssetDTO> ToDtoList(this List<Asset> entities);
    }
}
