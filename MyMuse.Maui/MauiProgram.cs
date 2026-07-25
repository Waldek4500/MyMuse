using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using MyMuse.Maui.Features.Home;
using MyMuse.Maui.Features.Player;
using MyMuse.Maui.Features.PlayerBar;
using MyMuse.Maui.Features.Search;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels;

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
            builder.Services.AddSingleton<PlaybackQueueService>();

            builder.Services.AddSingleton<MusicPlayerViewModel>();

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            builder.Services.AddTransient<PlayerPage>();
            builder.Services.AddTransient<PlayerPageViewModel>();

            builder.Services.AddSingleton<PlayerBar>();
            builder.Services.AddSingleton<PlayerBarViewModel>();

            builder.Services.AddSingleton<SearchPage>();
            builder.Services.AddSingleton<SearchPageViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
