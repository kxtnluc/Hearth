namespace Hearth.Core.DependencyInjection;
using Hearth.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddHearthCore(this IServiceCollection services, string dbPath)
    {
        services.AddDbContext<HearthDbContext>(options =>
            options.UseSqlite($"Data Source={dbPath}"));

        return services;
    }
}