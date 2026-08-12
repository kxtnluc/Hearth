using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.TransactionCategory;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Mapping.Finance
{
    [Mapper]
    internal static partial class TransactionCategoryRuleMapper
    {
        /// <summary>
        /// Turns a Model (entity) into its DTO
        /// </summary>
        public static partial TransactionCategoryRuleDTO ToDto(this TransactionCategoryRule entity);
        /// <summary>
        /// Turns a DTO back into its Model (entity)
        /// </summary>
        public static partial TransactionCategoryRule ToEntity(this TransactionCategoryRuleDTO dto);
        /// <summary>
        /// Turns a Model List into its DTO List counterpart
        /// </summary>
        public static partial List<TransactionCategoryRuleDTO> ToDtoList(this List<TransactionCategoryRule> entities);
        /// <summary>
        /// Applies non-null values from the DTO onto an existing tracked entity.
        /// Any property that's null on the DTO is left untouched on the entity.
        /// </summary>
        public static partial void ApplyUpdate(this TransactionCategoryRuleDTO dto, TransactionCategoryRule entity);
    }
}
