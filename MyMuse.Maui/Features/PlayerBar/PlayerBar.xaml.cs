using MyMuse.Maui.ViewModels;

namespace MyMuse.Maui.Features.PlayerBar;

public partial class PlayerBar : ContentView
{
	public PlayerBar()
	{
		InitializeComponent();

        BindingContext = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<PlayerBarViewModel>();
    }
}