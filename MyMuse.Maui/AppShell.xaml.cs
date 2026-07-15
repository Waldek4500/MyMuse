using MyMuse.Maui.Pages;

namespace MyMuse.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SongDetailsPage), typeof(SongDetailsPage));
        }
    }
}
