using MyMuse.Maui.Helpers;
using MyMuse.Maui.Models;
using MyMuse.Maui.ViewModels;
using System.ComponentModel;

namespace MyMuse.Maui.Services;

public sealed partial class NowPlayingSyncService : IDisposable
{
    private readonly MusicPlayerViewModel _player;

    /// <summary>
    /// List of methods that retruns <see cref="Song"/> type collection
    /// </summary>
    private readonly List<Func<IEnumerable<Song>>> _collectionAccessors = [];

    public NowPlayingSyncService(MusicPlayerViewModel player)
    {
        _player = player;
        _player.PropertyChanged += OnPlayerPropertyChanged;
    }

    /// <summary>
    /// Registers methods that must return <see cref="Song"/> type collection.
    /// </summary>
    /// <param name="collectionAccessors"></param>
    public void Track(params Func<IEnumerable<Song>>[] collectionAccessors)
        => _collectionAccessors.AddRange(collectionAccessors);


    /// <summary>
    /// Syncs <see cref="Song.IsPlaying"/> and <see cref="Song.IsSelected"/> properties state to match the currently playing song.
    /// </summary>
    public void SyncNow()
    {
        var currentId = _player.HasTrack ? _player.CurrentSong?.Id : null;
        var isPlaying = _player.IsPlaying;

        foreach(var getCollections in _collectionAccessors)
            SyncCollection(getCollections(), currentId, isPlaying);
    }

    /// <summary>
    /// Changes IsSelected and IsPlaying song's properties to the actual value
    /// </summary>
    /// <param name="songs">Collection of the songs to sync</param>
    /// <param name="currentSongId">Id of the currently playing song</param>
    private static void SyncCollection(IEnumerable<Song> songs, int? currentSongId, bool isPlaying)
    {
        foreach (Song song in songs)
        {
            if (currentSongId is null)
                song.IsSelected = song.IsPlaying = false;

            song.IsSelected = song.Data is not null && song.Data.Id == currentSongId;
            song.IsPlaying = song.Data is not null && song.Data.Id == currentSongId && isPlaying;
        }
    }


    /// <summary>
    /// Invokes the <see cref="SyncNow"/> method when the current Song or IsPlaying status change in the <see cref="MusicPlayerViewModel"/>
    /// </summary>
    /// <param name="sender">The object that invoked the event</param>
    /// <param name="e">The arguments of changed property</param>
    private void OnPlayerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(MusicPlayerViewModel.CurrentSong) &&
            e.PropertyName != nameof(MusicPlayerViewModel.IsPlaying))
            return;

        MainThread.BeginInvokeOnMainThread(SyncNow);
    }

    public void Dispose() => _player.PropertyChanged -= OnPlayerPropertyChanged;
}
