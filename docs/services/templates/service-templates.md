# Service Template (Finance)

This is a Service template from which most, if not all, Service files should be built.
Simply CTRL-H the key tags below, or go through manually, replacing what you need for the given page.
Variables marked with a `__` prefix/suffix are meant to be replaced.

### Full Template

```csharp
using Hearth.Core.Data;
using Hearth.Core.Models.Finance;
using Hearth.Services.Abstract;
using Hearth.Services.DTOs.Finance.Account;
using Hearth.Services.CTOs.Finance.Account;
using Hearth.Services.Utility;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Mapping.Finance;
using Microsoft.EntityFrameworkCore;
using Hearth.Services.Filters.Finance;

namespace Hearth.Services.Services.Finance
{
    public class __objectService : ASqliteTableService<__object, __objectDTO, SqliteTableFilter>, I__objectService
    {
        // Optional Imported Services
        private readonly I__importService ___importService;

        public AccountService(
            HearthDbContext context,
            I__importService __importService
        )
        : base(context)
        {
            ___importService = __importService;
        }
        #region Abstract Class Setup
        protected override DbSet<__object> DbSet => _context.__objects;
        protected override __objectDTO ToDto(__object entity) => entity.ToDto();
        protected override __object ToEntity(__objectDTO dto) => dto.ToEntity();
        protected override List<__objectDTO> ToDtoList(List<__object> entities) => entities.ToDtoList();
        protected override void ApplyUpdate(__objectDTO dto, __object entity) => dto.ApplyUpdate(entity);
        protected override void ValidatePayload(__objectDTO payload)
        {
            // To implement, simply fill out this function
            throw new NotImplementedException();
        }
        #endregion

        #region Filter
        public override List<__objectDTO> Filter(List<__objectDTO> __objects, SqliteTableFilter filter)
        {
            // To implement, create a unique Filter object, replace it in this service, and fill out this function
            throw new NotImplementedException();
        }
        #endregion

        #region Model Specific Functions

        #endregion
        #region CTO Generation

        #endregion
    }
}
```

## Placeholders to Replace

| Placeholder | Description |
| :---------: | ----------: |
| `__object` | The entity class this service manages (e.g. `Account`, `Transaction`) — from `Hearth.Core.Models.Finance` |
| `__objects` | The plural form used for the `DbSet` property and the `Filter` method's parameter name (e.g. `Accounts`, `Transactions`) |
| `__objectDTO` | The DTO type this service maps to/from (e.g. `AccountDTO`) — from `Hearth.Services.DTOs.Finance` |
| `__objectService` | The concrete service class name (e.g. `AccountService`) — also update the constructor name to match, since C# requires it |
| `I__objectService` | The interface this service implements (e.g. `IAccountService`) — must already declare `Filter` if `SqliteTableFilter` is swapped for a custom filter type |
| `I__importService` / `__importService` | An optional injected dependency this service needs beyond `HearthDbContext` (e.g. another service it composes with). Remove the field, constructor parameter, and using if not needed |
| `SqliteTableFilter` | The filter type used in the `ASqliteTableService<...>` generic and the `Filter` method signature. Swap for a dedicated filter (e.g. `AccountFilter`) once one exists for this entity — see Filter section below |

## Structure Notes

| Section | Description |
| :---------: | ----------: |
| Constructor | Fix the constructor's name to match the renamed class (the template shows `AccountService` as a leftover copy-paste artifact — this must be renamed or the file won't compile) |
| Abstract Class Setup | The five required overrides every concrete service needs: `DbSet`, `ToDto`, `ToEntity`, `ToDtoList`, `ApplyUpdate`, `ValidatePayload`. Each is a one-line delegation to the entity/DTO's Mapperly-generated methods, except `ValidatePayload`, which needs real logic (or can stay a `NotImplementedException` if this entity doesn't need creation/update validation) |
| Filter | Only needs implementing once a dedicated filter class exists for this entity (see `AccountFilter`/`TransactionFilter` for the pattern). Until then, leave the `NotImplementedException` in place rather than building filter logic against the generic `SqliteTableFilter`, which carries no entity-specific fields to filter on |
| Model Specific Functions | Entity-specific query methods that don't belong on every service — lookups by a unique field (`GetByItemId`, `GetByAccountNumber`), or methods needing eager-loaded related data (`GetByIdWithTransactions`) |
| CTO Generation | Methods that build this entity's corresponding CTO (e.g. `AccountCTO`), if one exists — typically a thin wrapper that fetches the DTO(s) needed and constructs the CTO with them |