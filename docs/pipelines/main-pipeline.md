# Hearth Data Pipeline

A quick reference for how data moves through Hearth, and the rules that keep it from breaking.

## Projects & Dependency Direction

```
Hearth.UI ──────────► Hearth.Services ──► Hearth.Core
    │                      ▲
    └──► Hearth.Integrations (Plaid)
```

| Project | Owns | Never references |
| :---------: | :------: | ----------: |
| Hearth.Core | Entities, `HearthDbContext`, migrations | Anything above it |
| Hearth.Services | DTOs, CTOs, Filters, Mappers, Services | Hearth.UI |
| Hearth.Integrations | Plaid HTTP calls + HttpResponse models | Hearth.Core |
| Hearth.UI | Pages, components, DI wiring | Hearth.Core |

**Rule:** only `Hearth.Services` touches entities. Everything else talks in DTOs through interfaces.

## The Layers

```
Entity (Core) ──Mapperly──► DTO (Services) ──Service──► UI
```

### 1. Entity — `Hearth.Core/Models`
Shape of the database. No display logic.

### 2. DTO — `Hearth.Services/DTOs`
What the UI sees. Computed/display props are DTO-only and ignored by the mapper.

| Suffix | Meaning |
| :---------: | ----------: |
| `_C` | Calculated value (bool, number). Never stored. |
| `_S` | Formatted display string. Never stored. |

```csharp
[MapperIgnore]
public bool IsIncome_C => Amount < 0;

[MapperIgnore]
public string Amount_S => CurrencySymbolHelper.ConvertDecimalToCurrency(Amount, Iso_Currency_Code);
```

Sensitive fields (e.g. `Access_Token`) do **not** exist on the general DTO. They are reached through a narrow service method like `GetAccessToken(itemId)`.

### 3. Mapper — `Hearth.Services/Mapping` (Mapperly)
One `internal static partial class` per entity.

```csharp
[Mapper]
internal static partial class AccountMapper
{
    public static partial AccountDTO ToDto(this Account entity);

    [MapperIgnoreSource(nameof(AccountDTO.Transactions))]
    [MapperIgnoreTarget(nameof(Account.Transactions))]
    public static partial Account ToEntity(this AccountDTO dto);

    public static partial List<AccountDTO> ToDtoList(this List<Account> entities);

    [MapperIgnoreSource(nameof(AccountDTO.Transactions))]
    [MapperIgnoreTarget(nameof(Account.Transactions))]
    public static partial void ApplyUpdate(this AccountDTO dto, Account entity);
}
```

- Ignore attributes go on the **single-object** method only, never on `ToDtoList`.
- Mapperly needs `[Mapper]` on the class and `partial` on both class and methods.
- Same-type mapping (`DTO → DTO`) returns the same reference. Clone with a `void CopyInto(source, target)` instead.

### 4. Service — `Hearth.Services/Services`
Every table service inherits `ASqliteTableService<TEntity, TDto, TFilter>` and implements a few one-line bridges:

```csharp
protected override DbSet<Account> DbSet => _context.Accounts;
protected override AccountDTO ToDto(Account entity) => entity.ToDto();
protected override Account ToEntity(AccountDTO dto) => dto.ToEntity();
protected override List<AccountDTO> ToDtoList(List<Account> entities) => entities.ToDtoList();
protected override void ApplyUpdate(AccountDTO dto, Account entity) => dto.ApplyUpdate(entity);
```

The base class provides `GetById`, `GetAll`, `Create`, `CreateRange`, `Update`, `UpdateRange`, `Delete`, `DeleteRange`, `Exists`.

Write methods take `bool saveChanges = true`. Pass `false` to batch several writes, then call `SaveChangesAsync()` once.

Concrete services stay `internal`, interfaces stay `public`, and everything is registered in `AddHearthServices`:

```csharp
services.AddScoped<IAccountService, AccountService>();
```

### 5. UI — `Hearth.UI`
Inject the interface, never the class.

```razor
@inject IAccountService _accountService
```

## Relationship Direction

Navigation only flows **down**. Never back up.

```
Account
  └─ Transactions
       └─ Category
            └─ Rules
                 └─ RuleConditions
```

- `Transaction` has `Account_Id` (string FK) but **no** `Account` navigation.
- Need a transaction's account? Look it up by `Account_Id`, ideally from a `Dictionary<string, AccountDTO>` loaded once.
- Two-way navigations = infinite Mapperly recursion = `StackOverflowException`.

Load the chain with `Include`:

```csharp
_context.Accounts
    .Include(a => a.Transactions)
        .ThenInclude(t => t.Category!)
            .ThenInclude(c => c.Rules)
                .ThenInclude(r => r.RuleConditions)
    .AsNoTracking();
```

## Tracking Rules (the important part)

`HearthDbContext` is **scoped** and shared. Any `SaveChangesAsync()` flushes **everything** staged in it, not just your change.

1. **All reads use `.AsNoTracking()`.**
2. **Never replace a child collection on a tracked entity.** Setting `account.Transactions = new()` on a tracked `Account` orphans every transaction. Required FK = EF **deletes them** on the next save.
3. **Child collections are written through their own service**, never through the parent's `ToEntity` / `ApplyUpdate`.
4. **Update relationships by FK**, not by assigning a navigation object:

```csharp
var transaction = await DbSet.FindAsync(transactionId);
transaction.CategoryId = categoryId;   // not transaction.Category = ...
await _context.SaveChangesAsync();
```

## Filters — `Hearth.Services/Filters`

A filter is a plain criteria class plus a static `BuildQuery`. Each `if` adds a `Where` (they AND together).

```csharp
if (!string.IsNullOrWhiteSpace(filter.Merchant_Name))
    query = query.Where(t =>
        t.Merchant_Name != null &&
        t.Merchant_Name.Contains(filter.Merchant_Name, StringComparison.OrdinalIgnoreCase));
```

- Use `IsNullOrWhiteSpace`, not `!= null` (cleared inputs become `""`).
- Dropdown `"any"` is normalized to `null` in the property setter.
- `filter.Clear()` (from `SqliteTableFilter`) resets every field via reflection.

## CTOs — `Hearth.Services/CTOs`

Computed report objects built from DTOs (averages, percentages, per-category sums).
Numbers live on the CTO as `_C`, formatted strings on a nested `_S` class:

```razor
@stats.Formated.Average_OutgoingPerPeriod_Num_S
```

## Plaid Integration — `Hearth.Integrations`

```
Plaid JSON ──► *HttpResponse model ──Mapperly──► DTO ──► Hearth.Services
```

- HttpResponse models mirror Plaid's JSON exactly and never leave `Hearth.Integrations`.
- Mapped straight to DTOs, then saved through services (`CreateRange`, `UpdateRange`).
- Sync loops on `has_more`, feeding `next_cursor` back in, and saves once per page.

## Migrations

Any entity or `OnModelCreating` change (including removing a navigation) needs a migration, or `Database.Migrate()` throws `PendingModelChangesWarning`.

```powershell
dotnet ef migrations add SomeChange --project Hearth.Core
```

While still in dev with test data, it's fine to squash: delete the whole `Migrations/` folder (snapshot included), delete `Hearth.db`, and add a fresh `InitialCreate`.

## Common Gotchas

| Symptom | Cause |
| :---------: | ----------: |
| Page crashes on load | Service not registered in DI, or `@inject` used a concrete class |
| `StackOverflowException` in a mapper | Two-way navigation between entities |
| Rows mysteriously deleted after a save | Child collection replaced on a tracked entity |
| `FOREIGN KEY constraint failed` | FK points to an Id that doesn't exist |
| Extension method "not found" | Missing `using`, or the mapper is `internal` and called from `Hearth.UI` |
| `RMG012` / `RMG020` warnings | DTO and entity property shapes don't match; add `[MapProperty]` or an ignore |