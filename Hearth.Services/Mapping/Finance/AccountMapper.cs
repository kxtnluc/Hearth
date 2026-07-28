using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Account;
using Riok.Mapperly.Abstractions;

namespace Hearth.Services.Mapping;

[Mapper]
internal static partial class AccountMapper
{
    public static partial AccountDTO ToDto(this Account entity);
    public static partial Account ToEntity(this AccountDTO dto);

    public static partial List<AccountDTO> ToDtoList(this List<Account> entities);
}