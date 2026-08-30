using MyMuse.Maui.Features.Album;
using MyMuse.Maui.Features.Player;
using MyMuse.Maui.Features.SearchResults;

namespace MyMuse.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PlayerPage), typeof(PlayerPage));
            Routing.RegisterRoute(nameof(SearchResultsPage), typeof(SearchResultsPage));
            Routing.RegisterRoute(nameof(AlbumPage), typeof(AlbumPage));
        }
    }
}
