using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Parcial2.Services;
using Parcial2.ViewModels;
using Parcial2.Views;
using Parcial2.DataBase;


namespace Parcial2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            // Desde la linea 18 hasta la 23 es lo que fue agregado para el segundo parcial
            string dbPath =
                Path.Combine(
                    FileSystem.AppDataDirectory,
                    "games.db3");

            builder.Services.AddSingleton(new GameDataBase(dbPath));
            //Esto permite que cualquier ViewModel
            //pueda usar la misma instancia de la base de datos
            //mediante la inyeccion de dependencias

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont(
                        "OpenSans-Regular.ttf",
                        "OpenSansRegular");

                    fonts.AddFont(
                        "OpenSans-Semibold.ttf",
                        "OpenSansSemibold");
                });

            // Services
            builder.Services.AddSingleton<ApiService>();
            
            // ViewModels
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<DetailViewModel>();
            builder.Services.AddTransient<AddGameViewModel>();

            // Views
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<DetailPage>();
            builder.Services.AddTransient<AddGamePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}