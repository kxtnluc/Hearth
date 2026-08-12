using Hearth.Core.Models;
using Hearth.Services.DTOs;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Mapping
{
    [Mapper]
    internal static partial class UserMapper
    {
        /// <summary>
        /// Turns a Model (entity) into its DTO
        /// </summary>
        public static partial UserDTO ToDto(this User entity);
        /// <summary>
        /// Turns a DTO back into its Model (entity)
        /// </summary>
        public static partial User ToEntity(this UserDTO dto);
        /// <summary>
        /// Turns a Model List into its DTO List counterpart
        /// </summary>
        public static partial List<UserDTO> ToDtoList(this List<User> entities);
        /// <summary>
        /// Applies non-null values from the DTO onto an existing tracked entity.
        /// Any property that's null on the DTO is left untouched on the entity.
        /// </summary>
        public static partial void ApplyUpdate(this UserDTO dto, User entity);
    }
}
