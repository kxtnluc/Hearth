using Hearth.Data;
using Microsoft.Extensions.Logging;
using Hearth.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Http;
namespace Hearth
{
    public static class MauiProgram
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static MauiApp CreateMauiApp()
        {


            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            // Define the database path
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "Hearth.db");

            // Register AppDbContext as a service with the dbPath
            builder.Services.AddSingleton<HearthDbContext>(_ => new HearthDbContext(dbPath));

            // Register QOL Service
            builder.Services.AddScoped<QolService>();

            // Initialize the database
            InitializeDatabase(dbPath);

            //Adding Plaid Service
            builder.Services.AddHttpClient<PlaidService>();
            builder.Services.AddScoped<PlaidService>();

            // Other Services
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            var app = builder.Build();
            ServiceProvider = app.Services;
            return app;
        }
        //Initalize the Database
        private static void InitializeDatabase(string dbPath)
        {
            System.Diagnostics.Debug.WriteLine($"Database path: {dbPath}"); // Debugging line
            using var context = new HearthDbContext(dbPath);
            context.Database.EnsureCreated();
        }
    }
}
