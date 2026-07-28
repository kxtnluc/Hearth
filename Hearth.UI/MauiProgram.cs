using Hearth.Core.Data;
using Hearth.Services.Data;
using Hearth.Services.DependencyInjection;
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

            // Hearth SQLite Database Service
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Hearth.db");
            builder.Services.AddHearthServices(dbPath);
            System.Diagnostics.Debug.WriteLine($"DB PATH: {dbPath}");

            // WebView
            builder.Services.AddMauiBlazorWebView();

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
