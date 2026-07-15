using MyMuse.Maui.ViewModels;

namespace MyMuse.Maui.Pages;

public partial class SongDetailsPage : ContentPage
{
	private readonly MusicPlayerViewModel _viewModel;

	public SongDetailsPage(MusicPlayerViewModel musicPlayerViewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = musicPlayerViewModel;
    }

	private void Slider_DragStarted(object sender, EventArgs e)
		=> _viewModel.BeginUserSeek();

    private void Slider_DragCompleted(object sender, EventArgs e)
	{
		if (sender is Slider slider)
			_viewModel.CompleteUserSeek(slider.Value);
	}
}