using MyMuse.Maui.Features.Player;

namespace MyMuse.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(PlayerPage), typeof(PlayerPage));
        }
    }
}
