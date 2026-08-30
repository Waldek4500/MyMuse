using MyMuse.Maui.Helpers;
using MyMuse.Maui.Models;
using MyMuse.Maui.ViewModels;
using System.ComponentModel;

namespace MyMuse.Maui.Services;

public sealed partial class NowPlayingSyncService : IDisposable
{
    private readonly MusicPlayerViewModel _player;
    private readonly List<Func<IEnumerable<Song>>> _collectionAccessors = [];

    public NowPlayingSyncService(MusicPlayerViewModel player)
    {
        _player = player;
        _player.PropertyChanged += OnPlayerPropertyChanged;
    }

    /// <summary>
    /// Registers methods that retruns <see cref="Song"/> type collection
    /// </summary>
    /// <param name="collectionAccessors"></param>
    public void Track(params Func<IEnumerable<Song>>[] collectionAccessors)
        => _collectionAccessors.AddRange(collectionAccessors);


    /// <summary>
    /// Syncs <see cref="Song.IsPlaying"/> and <see cref="Song.IsSelected"/> properties state to match currently playing song
    /// </summary>
    public void SyncNow()
    {
        var currentId = _player.HasTrack ? _player.CurrentSong?.Id : null;
        var isPlaying = _player.IsPlaying;

        foreach(var getCollections in _collectionAccessors)
            SongHelper.SyncCollection(getCollections(), currentId, isPlaying);
    }

    private void OnPlayerPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(MusicPlayerViewModel.CurrentSong) &&
            e.PropertyName != nameof(MusicPlayerViewModel.IsPlaying))
            return;

        MainThread.BeginInvokeOnMainThread(SyncNow);
    }

    public void Dispose() => _player.PropertyChanged -= OnPlayerPropertyChanged;
}
