# Account → Condition Pipeline

How the finance object graph is shaped, loaded, and mapped.

## The Chain

```
AccountDTO
  └─ List<TransactionDTO>          (Transactions)
       └─ TransactionCategoryDTO   (Category)
            └─ List<TransactionCategoryRuleDTO>   (Rules)
                 └─ List<RuleConditionDTO>        (RuleConditions)
```

**Navigation only flows down.** No child holds a navigation back to its parent.

| Child | Knows its parent by | Navigation back? |
| :---------: | :------: | ----------: |
| Transaction | `Account_Id` (Plaid string) | No |
| Category | — (Transaction holds `CategoryId`) | No |
| Rule | `TransactionCategoryId` | No |
| RuleCondition | `RuleId` | No |

## EF Relationships — `HearthDbContext`

```csharp
modelBuilder.Entity<Account>()
    .HasMany(a => a.Transactions)
    .WithOne()                                  // no Transaction.Account
    .HasForeignKey(t => t.Account_Id)
    .HasPrincipalKey(a => a.Account_Id);        // joins on Plaid string, not Id

modelBuilder.Entity<TransactionCategoryRule>()
    .HasOne<TransactionCategory>()
    .WithMany(c => c.Rules)
    .HasForeignKey(r => r.TransactionCategoryId);

modelBuilder.Entity<RuleCondition>()
    .HasOne<Rule>()
    .WithMany(r => r.RuleConditions)
    .HasForeignKey(rc => rc.RuleId);
```

`Transaction → Category` works by convention (`CategoryId` + `Category`).

## Loading the Full Chain

```csharp
var accounts = await _context.Accounts
    .Include(a => a.Transactions)
        .ThenInclude(t => t.Category!)
            .ThenInclude(c => c.Rules)
                .ThenInclude(r => r.RuleConditions)
    .AsNoTracking()
    .ToListAsync();

return accounts.ToDtoList();
```

- Always `AsNoTracking()` for reads.
- Only load as deep as the page needs. The full chain is heavy.
- If it gets slow, add `.AsSplitQuery()` after the includes.

## Mapping Rule

Collections map **down** in `ToDto`, and are **ignored** going back up (`ToEntity` / `ApplyUpdate`).

```csharp
[MapperIgnoreSource(nameof(AccountDTO.Transactions))]
[MapperIgnoreTarget(nameof(Account.Transactions))]
public static partial void ApplyUpdate(this AccountDTO dto, Account entity);
```

Same pattern on each mapper:

| Mapper | Ignore on `ToEntity` / `ApplyUpdate` |
| :---------: | ----------: |
| AccountMapper | `Transactions` |
| TransactionMapper | `Category` |
| TransactionCategoryMapper | `Rules` |
| Rule mapper | `RuleConditions` |

## Writing

Each level is written through **its own service**, never through its parent.

```csharp
await _transactionService.UpdateCategoryId(transactionId, categoryId); // by FK
await _ruleConditionService.Create(conditionDto);                      // own service
```

## Don'ts

- Don't add a parent navigation to a child (e.g. `Transaction.Account`). Infinite mapping recursion.
- Don't replace a collection on a tracked entity (`account.Transactions = new()`). EF deletes the orphans on the next save.
- Don't set a navigation object to change a relationship. Set the FK (`CategoryId`, `RuleId`).
- Need a transaction's account? Look it up by `Account_Id`.