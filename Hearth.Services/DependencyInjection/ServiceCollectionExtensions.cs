using Hearth.Core.DependencyInjection;
using Hearth.Services.Data;
using Hearth.Services.Interfaces;
using Hearth.Services.Interfaces.Finance;
using Hearth.Services.Services;
using Hearth.Services.Services.Finance;
using Microsoft.Extensions.DependencyInjection;

namespace Hearth.Services.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHearthServices(this IServiceCollection services, string dbPath)
    {
        services.AddHearthCore(dbPath);
        services.AddScoped<IDbInitializer, DbInitializer>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ITransactionService, TransactionService>();
        services.AddScoped<ITransactionCategoryRuleService, TransactionCategoryRuleService>();

        services.AddScoped<IRuleConditionService, RuleConditionService>();

        //services.AddScoped<IBankService, BankService>();


        return services;
    }
}