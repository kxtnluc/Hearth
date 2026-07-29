using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Account;
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
        public static partial TransactionDTO ToDto(this Transaction entity);
        public static partial Transaction ToEntity(this TransactionDTO dto);

        public static partial List<TransactionDTO> ToDtoList(this List<Transaction> entities);
    }
}
