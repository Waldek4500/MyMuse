using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels;

namespace MyMuse.Maui.Views;

public partial class PlayerEngineHost : ContentView
{
    private readonly MusicPlayerViewModel _playerViewModel;
    private readonly PlaybackQueueService _playbackQueueService;

	public PlayerEngineHost()
	{
		InitializeComponent();

        _playerViewModel = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<MusicPlayerViewModel>();
        _playbackQueueService = Application.Current!.Handler!.MauiContext!.Services.GetRequiredService<PlaybackQueueService>();

        BindingContext = _playerViewModel;

        _playerViewModel.PlayRequested += () => AudioPlayer.Play();
        _playerViewModel.PauseRequested += () => AudioPlayer.Pause();
        _playerViewModel.SliderSeekCompleted += async (_, position) =>
        {
            await AudioPlayer.SeekTo(position, CancellationToken.None);
            AudioPlayer.Play();
        };
    }
    private void AudioPlayer_StateChanged(object sender, CommunityToolkit.Maui.Core.MediaStateChangedEventArgs e)
    => _playerViewModel.State = e.NewState;

    /// <summary>
    /// Execute UpdatePosition when song postion was changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void AudioPlayer_PositionChanged(object sender, CommunityToolkit.Maui.Core.MediaPositionChangedEventArgs e)
        => _playerViewModel.UpdatePostion(e.Position);

    /// <summary>
	/// Executes UpdateDuration when song was loaded 
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    void AudioPlayer_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (AudioPlayer is null || _playerViewModel is null)
            return;

        _playerViewModel.UpdateDuration(AudioPlayer.Duration);
    }

    private void AudioPlayer_MediaEnded(object sender, EventArgs e) => _playbackQueueService.GoToNextSongWhenMusicOver();
}