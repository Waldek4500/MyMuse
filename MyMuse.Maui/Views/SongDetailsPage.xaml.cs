using MyMuse.Maui.ViewModels;

namespace MyMuse.Maui.Views;

public partial class SongDetailsPage : ContentPage
{
	SongDetailsPageViewModel _viewModel;

	public SongDetailsPage(SongDetailsPageViewModel songDetailsPageViewModel)
	{
		InitializeComponent();
		BindingContext = _viewModel = songDetailsPageViewModel;

		_viewModel.PlayRequested += (_,_) => AudioPlayer.Play();
		_viewModel.PauseRequested += (_,_) => AudioPlayer.Pause();
		_viewModel.SliderSeekCompleted += async (_, position) =>
		{
			await AudioPlayer.SeekTo(position, CancellationToken.None);
			AudioPlayer.Play();
		};

    }

    private void AudioPlayer_StateChanged(object sender, CommunityToolkit.Maui.Core.MediaStateChangedEventArgs e)
		=> _viewModel.State = e.NewState;

	/// <summary>
	/// Execute UpdatePosition when song postion was changed
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    private void AudioPlayer_PositionChanged(object sender, CommunityToolkit.Maui.Core.MediaPositionChangedEventArgs e)
		=> _viewModel.UpdatePostion(e.Position);

	private void Slider_DragStarted(object sender, EventArgs e)
		=> _viewModel.BeginUserSeek();

    private void Slider_DragCompleted(object sender, EventArgs e)
	{
		if (sender is Slider slider)
			_viewModel.CompleteUserSeek(slider.Value);
	}

	/// <summary>
	/// Executes UpdateDuration when song was loaded 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    void AudioPlayer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		if (AudioPlayer is null || _viewModel is null)
			return;

		_viewModel.UpdateDuration(AudioPlayer.Duration);
	}

}