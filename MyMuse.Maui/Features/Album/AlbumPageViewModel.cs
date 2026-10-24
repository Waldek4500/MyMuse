using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Features.Player;
using MyMuse.Maui.Helpers;
using MyMuse.Maui.Models;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Shared.Dtos;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyMuse.Maui.Features.Album;

public partial class AlbumPageViewModel: BaseViewModel, IQueryAttributable
{
    #region Private Members
    private readonly PlaybackQueueService _queueService;
    private readonly AlbumsService _albumsService;
    private readonly SongsService _songsService;
    private readonly NowPlayingSyncService _syncService;
    private readonly MusicPlayerViewModel _player;
    private readonly Random random = new();
    #endregion

    #region Public Properties
    // The full list of tracks
    [ObservableProperty]
    public partial ObservableCollection<Song> Songs { get; set; } = [];

    // Album info
    [ObservableProperty]
    public partial AlbumDto? Album { get; set; }

    // Full duration of album's tracks in text
    [ObservableProperty]
    public partial string FullDurationText { get; set; } = String.Empty;
    #endregion

    #region Constructor
    public AlbumPageViewModel(AlbumsService albumsService, SongsService songsService,
                              PlaybackQueueService queueService, NowPlayingSyncService syncService,
                              MusicPlayerViewModel player)
    {
        _albumsService = albumsService;
        _songsService = songsService;
        _queueService = queueService;
        _syncService = syncService;
        _player = player;

        _syncService.Track(() => Songs);
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Gets an album with given id from an API.
    /// </summary>
    /// <param name="id">Album id</param>
    /// <returns></returns>
    private async Task GetAlbumAsync(int id)
    {
        try
        {
            IsBusy = true;
            Album = await _albumsService.GetByIdAsync(id);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"==Error in GetAlbumAsync: {ex}");
        }
        finally
        {
            IsBusy = false; 
        }
    }

    /// <summary>
    /// Gets all songs for an album when it's changed.
    /// </summary>
    /// <param name="value">New album value</param>
    async partial void OnAlbumChanged(AlbumDto? value)
    {
        try
        {
            if (value is null)
                return;

            Songs = [.. (await _songsService
                .GetAllFromAlbum(value.Id))
                .Select((song, index) => new Song(song) { Number = index + 1})];

            // Syncs wich song is currently playing
            _syncService.SyncNow();

            TimeSpan fullDuration = TimeSpan.FromSeconds(Songs.Sum(song => song.Data.Duration));
            FullDurationText = TimeHelper.FormatTime(fullDuration);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"==Error in GetAlbumAsync: {ex}");
        }
    }

    /// <summary>
    /// Navigates to specific track
    /// </summary>
    /// <param name="song">The song we navigate to</param>
    /// <param name="shouldShuffleQueue">Should the playback queue be in random order</param>
    /// <returns></returns>
    private async Task NavigateToSongAsync(Song song, bool shouldShuffleQueue = false)
    {
        await Shell.Current.GoToAsync(nameof(PlayerPage), true,
            new Dictionary<string, object>
            {
                {"Song", song.Data }
            });

        _queueService.PreviousSongs.Clear();

        // Sets in a sequence previous songs and the playback queue according to an album order
        if (!shouldShuffleQueue)
        {
            for (int i = 0; i < song.Number - 1; i++)
                _queueService.PreviousSongs.Push(Songs[i].Data);

            _queueService.SongsQueue = [.. Songs.Skip(song.Number).Select(s => s.Data)];
        }

        // Sets in a random order the playback queue without previous songs
        if (shouldShuffleQueue)
            _queueService.SongsQueue = [.. Songs.Where(s => s.Data.Id != song.Data.Id).Shuffle().Select(s => s.Data)];
        
    }
    #endregion

    #region Commands
    [RelayCommand]
    private async Task GoToSongDetailsAsync(Song song) => await NavigateToSongAsync(song);

    [RelayCommand]
    private async Task PlayAlbumAsync() => await NavigateToSongAsync(Songs[0]);

    [RelayCommand]
    private async Task PlayAlbumInRandomOrderAsync() => await NavigateToSongAsync(Songs[random.Next(0, Songs.Count)], true);

    #endregion

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("AlbumId", out var value) &&
            int.TryParse(value.ToString(), out int albumId))
        {
            await GetAlbumAsync(albumId);
        }            
    }
}
