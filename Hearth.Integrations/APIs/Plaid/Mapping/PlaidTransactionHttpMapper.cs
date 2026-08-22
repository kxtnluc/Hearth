using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Services.DTOs.Finance.Transaction;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Mapping;

[Mapper]
internal static partial class PlaidTransactionHttpMapper
{
    //TODO some mapper error going on here. fix later
    [MapperIgnoreTarget(nameof(TransactionDTO.Id))]
    [MapperIgnoreTarget(nameof(TransactionDTO.CategoryId))]
    public static partial TransactionDTO ToDto(this PlaidTransactionHttpResponse source);
    public static partial List<TransactionDTO> ToDtoList(this List<PlaidTransactionHttpResponse> source);
}
