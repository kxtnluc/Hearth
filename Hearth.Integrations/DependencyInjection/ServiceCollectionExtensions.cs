using Hearth.Integrations.APIs.Plaid;
using Hearth.Integrations.APIs.Plaid.Interfaces;
using Hearth.Integrations.APIs.Plaid.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hearth.Integrations.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHearthIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<PlaidOptions>(configuration.GetSection("Plaid"));

            services.AddHttpClient<IPlaidService, PlaidService>();
            services.AddHttpClient<IPlaidTransactionService, PlaidTransactionService>();
            services.AddHttpClient<IPlaidAccountService, PlaidAccountService>();

            return services;
        }
    }
}
