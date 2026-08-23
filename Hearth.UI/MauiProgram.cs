using Hearth.Core.Data;
using Hearth.Integrations.APIs.Plaid;
using Hearth.Integrations.DependencyInjection;
using Hearth.Services.Data;
using Hearth.Services.DependencyInjection;
using Hearth.Services.Interfaces;
using Hearth.UI.Interfaces.Plaid;
using Hearth.UI.Platform;
using Hearth.UI.Services.Plaid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Hearth.UI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            // Load appsettings.json as an embedded asset
            using var stream = FileSystem.OpenAppPackageFileAsync("appsettings.json").GetAwaiter().GetResult();
            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);

            // Hearth SQLite Database & All Services
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Hearth.db");
            builder.Services.AddHearthServices(dbPath);
            System.Diagnostics.Debug.WriteLine($"DB PATH: {dbPath}");

            // Hearth .Integrations
            builder.Services.AddHearthIntegrations(builder.Configuration);

            builder.Services.Configure<PlaidOptions>(builder.Configuration.GetSection("Plaid"));

            // Hearth .UI Services
            builder.Services.AddScoped<IPlaidLinkService, PlaidLinkService>();

            // WebView
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddSingleton<ISecureStorageProvider, MauiSecureStorageProvider>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var initializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
                    initializer.Initialize();
                    System.Diagnostics.Debug.WriteLine("DB INIT SUCCEEDED");
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"DB INIT FAILED: {ex}");
                }
            }

            return app;
        }
    }
}
