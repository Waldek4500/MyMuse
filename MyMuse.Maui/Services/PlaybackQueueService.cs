using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.ViewModels;
using MyMuse.Shared.Dtos;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyMuse.Maui.Services;

public partial class PlaybackQueueService
{
    private const int MinQueueSize = 4;

    private readonly MusicPlayerViewModel _player;
    private readonly SongsService _songsService;

    public ObservableCollection<SongDto> SongsQueue { get; set; } = [];
    public Stack<SongDto> PreviousSongs { get; set; } = [];

    public PlaybackQueueService(MusicPlayerViewModel player, SongsService songsService)
    {
        _player = player;
        _songsService = songsService;

        // Subscribes the player's SongChanged event
        _player.SongChanged += async (_, song) =>
        {
            if(SongsQueue.Count < MinQueueSize)
                await FillQueueAsync(song);
        };
    }

    public void GoToNextSongWhenMusicOver()
    {
        if (_player.ShouldPlayInLoop)
            return;

        NextSong();
    }


    /// <summary>
    /// Fills the queue with random tracks without the currently playing one.
    /// </summary>
    /// <param name="currentSong">The currenlty playing song</param>
    public async Task FillQueueAsync(SongDto currentSong)
    {
        try
        {
            List<SongDto> songsToQueue = await _songsService.GetRandomAsync(10);

            if (SongsQueue.Count == 0)
                // Assures that the currently playing song won't be repeated in the new queue
                songsToQueue.Remove(currentSong);
            else
                // Assures that the last track from the remaining queue won't be repeated as first in the new queue
                songsToQueue.Remove(SongsQueue.Last());

            foreach (SongDto song in songsToQueue)
                SongsQueue.Add(song);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"===Error in FillQueueAsync: {ex}");
        }

    }

    /// <summary>
    /// Skips to the next song from the queue and adds previous one to the <see cref="PreviousSongs"/>.
    /// </summary>
    [RelayCommand]
    private void NextSong()
    {
        if (SongsQueue.Count > 0 && _player.CurrentSong is not null)
        {
            PreviousSongs.Push(_player.CurrentSong);
            _player.CurrentSong = SongsQueue[0];
            SongsQueue.RemoveAt(0);
        }
    }

    /// <summary>
    /// Retruns to the previous song.
    /// </summary>
    [RelayCommand]
    private void PreviousSong()
    {
        if (PreviousSongs.TryPop(out SongDto? song) && _player.CurrentSong is not null)
        {
            // Add current song to the start of queue and then play previous
            SongsQueue.Insert(0, _player.CurrentSong);
            _player.CurrentSong = song;
        }
        else
            // If there is no previous tracks
            _player.SeekToAndResume(0);
    }

    [RelayCommand]
    private void TogglePlayInLoop() => _player.ShouldPlayInLoop = !_player.ShouldPlayInLoop;
}
