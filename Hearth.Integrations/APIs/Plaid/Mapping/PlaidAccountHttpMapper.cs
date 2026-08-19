using Hearth.Integrations.APIs.Plaid.HttpResponses;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.DTOs.Finance.Transaction;
using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.APIs.Plaid.Mapping;

[Mapper]
internal static partial class PlaidAccountHttpMapper
{
    [MapperIgnoreTarget(nameof(AccountDTO.Id))]
    [MapperIgnoreTarget(nameof(AccountDTO.IsOpen))]
    [MapperIgnoreTarget(nameof(AccountDTO.Bank_Item_Id))]

    public static partial AccountDTO ToDto(this PlaidAccountHttpResponse source);
    public static partial List<AccountDTO> ToDtoList(this List<PlaidAccountHttpResponse> source);
}
