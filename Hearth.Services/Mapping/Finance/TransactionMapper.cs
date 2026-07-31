using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Transaction;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Mapping.Finance
{
    [Mapper]
    internal static partial class TransactionMapper
    {
        /// <summary>
        /// Turns a Model (entity) into its DTO
        /// </summary>
        public static partial TransactionDTO ToDto(this Transaction entity);
        /// <summary>
        /// Turns a DTO back into its Model (entity)
        /// </summary>
        public static partial Transaction ToEntity(this TransactionDTO dto);
        /// <summary>
        /// Turns a Model List into its DTO List counterpart
        /// </summary>
        public static partial List<TransactionDTO> ToDtoList(this List<Transaction> entities);
        /// <summary>
        /// Applies non-null values from the DTO onto an existing tracked entity.
        /// Any property that's null on the DTO is left untouched on the entity.
        /// </summary>
        public static partial void ApplyUpdate(this TransactionDTO dto, Transaction entity);
    }
}
