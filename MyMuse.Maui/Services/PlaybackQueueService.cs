using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.ViewModels;
using MyMuse.Shared.Dtos;
using System.Diagnostics;

namespace MyMuse.Maui.Services;

public partial class PlaybackQueueService
{
    private readonly MusicPlayerViewModel _player;
    private readonly SongsService _songsService;

    public Queue<SongDto> SongsQueue { get; set; } = [];
    public Stack<SongDto> PreviousSongs { get; set; } = [];


    public PlaybackQueueService(MusicPlayerViewModel player, SongsService songsService)
    {
        _player = player;
        _songsService = songsService;

        _player.SongChanged += async (_, song) =>
        {
            SongsQueue.Clear();
            await FillQueueAsync(song);
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
            List<SongDto> songs = await _songsService.GetRandomSongsFromBandAsync(currentSong.BandId, 10);
            songs.Remove(currentSong);

            foreach (SongDto song in songs)
                SongsQueue.Enqueue(song);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"===Error in FillQueueAsync: {ex}");
        }

    }

    [RelayCommand]
    private void NextSong()
    {
        if (SongsQueue.TryDequeue(out SongDto? song) && _player.CurrentSong is not null)
        {
            PreviousSongs.Push(_player.CurrentSong);
            _player.CurrentSong = song;
        }
    }

    [RelayCommand]
    private void PreviousSong()
    {
        if (PreviousSongs.TryPop(out SongDto? song) && _player.CurrentSong is not null)
        {
            SongsQueue.Enqueue(_player.CurrentSong);
            _player.CurrentSong = song;
        }
    }

    [RelayCommand]
    private void TogglePlayInLoop() => _player.ShouldPlayInLoop = !_player.ShouldPlayInLoop;
}
