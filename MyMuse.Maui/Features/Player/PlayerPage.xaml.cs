using MyMuse.Maui.ViewModels;

namespace MyMuse.Maui.Features.Player;

public partial class PlayerPage : ContentPage
{
	private readonly PlayerPageViewModel _playerPageViewModel;

	public PlayerPage(PlayerPageViewModel playerPageViewModel)
	{
		InitializeComponent();
		BindingContext = _playerPageViewModel = playerPageViewModel;
    }

	private void Slider_DragStarted(object sender, EventArgs e)
		=> _playerPageViewModel.Player.BeginUserSeek();

    private void Slider_DragCompleted(object sender, EventArgs e)
	{
		if (sender is Slider slider)
			_playerPageViewModel.Player.CompleteUserSeek(slider.Value);
	}
}