using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyMuse.Maui.Features.Home;
using MyMuse.Maui.Features.Search;
using MyMuse.Maui.Models;
using MyMuse.Maui.Pages;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels;
using MyMuse.Maui.Views;

namespace MyMuse.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitMediaElement(true)
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Archivo-VariableFont.ttf", "Archivo");
                    fonts.AddFont("DMSans.ttf", "DMSans");
                    fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialSymbols");
                });

            builder.Services.AddSingleton<SongsService>();
            builder.Services.AddSingleton<BandsService>();
            builder.Services.AddSingleton<GenresService>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<SongDetailsPage>();
            builder.Services.AddSingleton<MusicPlayerViewModel>();
            builder.Services.AddSingleton<MiniPlayerBar>();

            builder.Services.AddSingleton<SearchPage>();
            builder.Services.AddSingleton<SearchPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
