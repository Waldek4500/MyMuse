using CommunityToolkit.Maui.Views;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

namespace MyMuse.Maui.Views;

public partial class PlayerEngineHost : ContentView
{
    private readonly MusicPlayerViewModel _playerViewModel;
    private readonly PlaybackQueueService _playbackQueueService;

	public PlayerEngineHost(MusicPlayerViewModel musicPlayerViewModel, PlaybackQueueService playbackQueueService)
	{
		InitializeComponent();

        _playerViewModel = musicPlayerViewModel;
        _playbackQueueService = playbackQueueService;

        BindingContext = _playerViewModel;

        _playerViewModel.PlayRequested += () => AudioPlayer.Play();
        _playerViewModel.PauseRequested += () => AudioPlayer.Pause();
        _playerViewModel.SliderSeekCompleted += async (_, position) =>
        {
            await AudioPlayer.SeekTo(position, CancellationToken.None);
            AudioPlayer.Play();
        };
    }

    /// <summary>
    /// Updates the <see cref="MusicPlayerViewModel.State"/> when the <see cref="MediaElement"/> state's changed.
    /// </summary>
    private void AudioPlayer_StateChanged(object sender, CommunityToolkit.Maui.Core.MediaStateChangedEventArgs e)
    => _playerViewModel.State = e.NewState;

    /// <summary>
    /// Executes the <see cref="MusicPlayerViewModel.UpdatePostion(TimeSpan)"/> when the song's postion was changed
    /// </summary>
    private void AudioPlayer_PositionChanged(object sender, CommunityToolkit.Maui.Core.MediaPositionChangedEventArgs e)
        => _playerViewModel.UpdatePostion(e.Position);

    /// <summary>
	/// Executes the <see cref="MusicPlayerViewModel.UpdateDuration(TimeSpan)"/> when the song's duration changed.
	/// </summary>
    void AudioPlayer_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (AudioPlayer is null || _playerViewModel is null || e.PropertyName != "Duration")
            return;

        _playerViewModel.UpdateDuration(AudioPlayer.Duration);
    }

    private void AudioPlayer_MediaEnded(object sender, EventArgs e) => _playbackQueueService.GoToNextSongWhenMusicOver();

    /// <summary>
    /// Starts song's playback after it's first opened
    /// </summary>
    private void AudioPlayer_MediaOpened(object sender, EventArgs e)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            if (AudioPlayer.Handler is null || !_playerViewModel.ShoudAutoPlay)
                return;

            Debug.WriteLine("Buffering 2:" + _playerViewModel.IsBuffering);
            AudioPlayer.Play();
            Debug.WriteLine("Buffering 3:" + _playerViewModel.IsBuffering);
            _playerViewModel.ShoudAutoPlay = false;
        });
    }
}