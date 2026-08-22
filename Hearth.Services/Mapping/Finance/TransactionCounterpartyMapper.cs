using Hearth.Core.Models.Finance.ValueObjects;
using Hearth.Services.DTOs.Finance.Transaction;
using Riok.Mapperly.Abstractions;

namespace Hearth.Services.Mapping.Finance;

[Mapper]
internal static partial class TransactionCounterpartyMapper
{
    [MapperIgnoreSource(nameof(TransactionCounterparty.Id))]
    public static partial CounterpartyDTO ToDto(this TransactionCounterparty entity);

    [MapperIgnoreTarget(nameof(TransactionCounterparty.Id))]   // EF assigns this on insert
    public static partial TransactionCounterparty ToEntity(this CounterpartyDTO dto);

    public static partial List<CounterpartyDTO> ToDtoList(this List<TransactionCounterparty> entities);
    public static partial List<TransactionCounterparty> ToEntityList(this List<CounterpartyDTO> dtos);
}