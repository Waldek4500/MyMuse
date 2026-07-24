using MyMuse.Maui.Pages;
using MyMuse.Maui.ViewModels;

namespace MyMuse.Maui.Views;

public partial class MiniPlayerBar : ContentView
{
    private readonly MusicPlayerViewModel _viewModel;
	public MiniPlayerBar()
	{
		InitializeComponent();

        _viewModel = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<MusicPlayerViewModel>();
        BindingContext = _viewModel;
    }
}