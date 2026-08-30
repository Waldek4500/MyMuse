using MyMuse.Maui.ViewModels;

namespace MyMuse.Maui.Features.Player;

public partial class PlayerPage : ContentPage
{
	public PlayerPage(PlayerPageViewModel playerPageViewModel)
	{
		InitializeComponent();
		BindingContext = playerPageViewModel;
    }
}