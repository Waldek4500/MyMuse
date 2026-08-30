using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.ViewModels;
using MyMuse.Shared.Dtos;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyMuse.Maui.Services;

public partial class PlaybackQueueService
{
    private readonly MusicPlayerViewModel _player;
    private readonly SongsService _songsService;

    public ObservableCollection<SongDto> SongsQueue { get; set; } = [];
    public Stack<SongDto> PreviousSongs { get; set; } = [];

    public PlaybackQueueService(MusicPlayerViewModel player, SongsService songsService)
    {

        _player = player;
        _songsService = songsService;

        _player.SongChanged += async (_, song) =>
        {
            if(SongsQueue.Count < 4)
                await FillQueueAsync(song);

            foreach (var s in SongsQueue)
                Debug.WriteLine(s);
        };
    }

    public void GoToNextSongWhenMusicOver()
    {
        if (_player.ShouldPlayInLoop)
            return;

        NextSong();
    }

    public async Task FillQueueAsync(SongDto currentSong)
    {
        try
        {
            List<SongDto> songs = await _songsService.GetRandomAsync(10);

            if (SongsQueue.Count == 0)
                songs.Remove(currentSong);
            else
                songs.Remove(SongsQueue.Last());

            foreach (SongDto song in songs)
                SongsQueue.Add(song);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"===Error in FillQueueAsync: {ex}");
        }

    }

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

    [RelayCommand]
    private void PreviousSong()
    {
        if (PreviousSongs.TryPop(out SongDto? song) && _player.CurrentSong is not null)
        {
            // Add to start
            SongsQueue.Insert(0, _player.CurrentSong);
            _player.CurrentSong = song;
        }
        else
            _player.SeekToAndResume(0);
    }

    [RelayCommand]
    private void TogglePlayInLoop() => _player.ShouldPlayInLoop = !_player.ShouldPlayInLoop;
}
