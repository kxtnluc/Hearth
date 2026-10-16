using Hearth.Core.Models.Finance;
using Hearth.Services.DTOs.Finance.Account;
using Riok.Mapperly.Abstractions;

namespace Hearth.Services.Mapping.Finance;

[Mapper]
[UseStaticMapper(typeof(TransactionMapper))]
internal static partial class AccountMapper
{
    /// <summary>
    /// Turns a Model (entity) into its DTO. Transactions map through TransactionMapper.
    /// </summary>
    public static partial AccountDTO ToDto(this Account entity);

    /// <summary>
    /// Turns a DTO back into its Model (entity). Transactions are never written back
    /// through an account; they are managed by TransactionService.
    /// </summary>
    [MapperIgnoreSource(nameof(AccountDTO.Transactions))]
    [MapperIgnoreTarget(nameof(Account.Transactions))]
    public static partial Account ToEntity(this AccountDTO dto);

    /// <summary>
    /// Turns a Model List into its DTO List counterpart
    /// </summary>
    public static partial List<AccountDTO> ToDtoList(this List<Account> entities);

    /// <summary>
    /// Applies DTO values onto an existing tracked entity. Transactions are ignored
    /// so a tracked Account's collection is never replaced (which would orphan-delete them).
    /// </summary>
    [MapperIgnoreSource(nameof(AccountDTO.Transactions))]
    [MapperIgnoreTarget(nameof(Account.Transactions))]
    public static partial void ApplyUpdate(this AccountDTO dto, Account entity);

    /// <summary>
    /// Deep-copies every property from source onto target — used to implement Clone()
    /// without Mapperly's same-type identity-mapping shortcut kicking in.
    /// </summary>
    public static partial void CopyInto(this AccountDTO source, AccountDTO target);
}