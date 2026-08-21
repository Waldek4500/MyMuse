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
using System.ComponentModel;
using System.Diagnostics;

namespace MyMuse.Maui.Features.Home
{
    public partial class MainPageViewModel : BaseViewModel
    {
        #region Private Members
        private readonly SongsService _songsService;
        private readonly ArtistsService _bandsService;
        private readonly MusicPlayerViewModel _musicPlayerViewModel;
        #endregion

        #region Public Properties
        [ObservableProperty]
        public partial ObservableCollection<Song> FeaturedSongs { get; set; } = [.. new Song[4]];

        [ObservableProperty]
        public partial ObservableCollection<Song> RecentSongs { get; set; } = [.. new Song[4]];

        [ObservableProperty]
        public partial ObservableCollection<Song> TrendingSongs { get; set; } = [.. new Song[5]];

        [ObservableProperty]
        public partial ObservableCollection<ArtistDto> Artists { get; set; } = [.. new ArtistDto[4]];

        [ObservableProperty]
        public partial bool[] ShowPlaceHolder { get; set; } = { true, true, true, true };

        [ObservableProperty]
        public partial bool IsRefreshing { get; set; }
        #endregion

        #region Constructor
        public MainPageViewModel(SongsService songsService, ArtistsService artistsService, MusicPlayerViewModel musicPlayerViewModel)
        {
            _songsService = songsService;
            _bandsService = artistsService;
            _musicPlayerViewModel = musicPlayerViewModel;

            _musicPlayerViewModel.PropertyChanged += OnMusicPlayerViewModelPropertyChanged;
        }
        #endregion

        #region Methods
        public async Task InitializeAsync() => await GetSongsAsync();

        /// <summary>
        /// Changes IsSelected and IsPlaying song's properties for each collection to correct value by invoking SyncCollection method
        /// </summary>
        private void SyncIsPlayingFlags()
        {
            if (!_musicPlayerViewModel.HasTrack || _musicPlayerViewModel.CurrentSong is null)
                return;

            var curentId = _musicPlayerViewModel.CurrentSong.Id;
            var isPlaying = _musicPlayerViewModel.IsPlaying;

            SongHelper.SyncCollection(FeaturedSongs, curentId, isPlaying);
            SongHelper.SyncCollection(RecentSongs, curentId, isPlaying);
            SongHelper.SyncCollection(TrendingSongs, curentId, isPlaying);
        }

        private void OnMusicPlayerViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != nameof(MusicPlayerViewModel.CurrentSong) && e.PropertyName != nameof(MusicPlayerViewModel.IsPlaying))
                return;

            MainThread.BeginInvokeOnMainThread(SyncIsPlayingFlags);
        }
        #endregion

        #region Commands
        /// <summary>
        /// Gets all songs from service and assigns it to ObservableCollections
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task GetSongsAsync()
        {
            try
            {
                IsRefreshing = false;
                IsBusy = true;

                FeaturedSongs = [.. (await _songsService.GetFeaturedAsync(4)).Select(song => new Song(song)) ];
                ShowPlaceHolder[0] = false;

                RecentSongs = [.. (await _songsService.GetRecentAsync()).Select(song => new Song(song))];
                ShowPlaceHolder[1] = false;

                TrendingSongs = [.. (await _songsService.GetTrendingAsync(5)).Select(song => new Song(song))];
                ShowPlaceHolder[2] = false;

                Artists = [.. await _bandsService.GetBandsAsync()];
                ShowPlaceHolder[3] = false;

                SyncIsPlayingFlags();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"====GetSongsAsync Error: {ex}");

                bool isAccepted = await Shell.Current.DisplayAlertAsync("Error occurred", "We coudn't provide you songs.", "Try again", "Cancel");
                
                if(isAccepted || ShowPlaceHolder[0])
                    await GetSongsAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Navigates to song details page
        /// </summary>
        /// <param name="song">Selected song</param>
        /// <returns></returns>
        [RelayCommand]
        private async Task GoToSongDetailsAsync(SongDto song)
        {
            if (song is null)
                return;

            await Shell.Current.GoToAsync(nameof(PlayerPage), true,
                new Dictionary<string, object>
                {
                    {"Song", song }
                }
            );
        }
        #endregion
    }
}
