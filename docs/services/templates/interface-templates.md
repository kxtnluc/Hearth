# Interface Template (Finance)

This is a Interface Service template from which most, if not all, Interface pages should be built.
Simply CTRL-H the key tags below, or go through manually, replacing what you need for the given page.
Variables marked with a `__` prefix/suffix are meant to be replaced.

### Full Template

```csharp
using Hearth.Services.DTOs.Finance.__object;
using Hearth.Services.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Services.Interfaces.Finance;

public interface I__objectService : ISqliteTableService<__objectDTO, SqliteTableFilter>
{
    // Custom Interface Service Functions
}


```


## DI <----------------

NOTE: pages will crash without any error if the interface/service aren't marked for DI in:
Hearth\Hearth.Services\DependencyInjection\ServiceCollectionExtensions.cs

```csharp
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHearthServices(this IServiceCollection services, string dbPath)
    {
        services.AddHearthCore(dbPath);
        services.AddScoped<IDbInitializer, DbInitializer>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITransactionCategoryRuleService, TransactionCategoryRuleService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ISessionService, SessionService>();
        services.AddScoped<IRuleConditionService, RuleConditionService>();
        services.AddScoped<IBankService, BankService>();
        services.AddScoped<ITransactionSyncRecordService, TransactionSyncRecordService>();
        // ADD Service here

        return services;
    }
}
```
