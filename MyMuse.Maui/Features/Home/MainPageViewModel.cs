using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Features.Album;
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
        private readonly AlbumsService _albumsService;
        private readonly NowPlayingSyncService _syncService;
        #endregion

        #region Public Properties
        [ObservableProperty]
        public partial ObservableCollection<Song> FeaturedSongs { get; set; } = [.. new Song[4]];

        [ObservableProperty]
        public partial ObservableCollection<Song> RecentSongs { get; set; } = [.. new Song[4]];

        [ObservableProperty]
        public partial ObservableCollection<AlbumDto> Albums { get; set; } = [.. new AlbumDto[4]];

        [ObservableProperty]
        public partial ObservableCollection<Song> TrendingSongs { get; set; } = [.. new Song[5]];

        [ObservableProperty]
        public partial ObservableCollection<ArtistDto> Artists { get; set; } = [.. new ArtistDto[4]];

        /// <summary>
        /// Placeholders for each section (5)
        /// </summary>
        [ObservableProperty]
        public partial bool[] ShowPlaceHolder { get; set; } = { true, true, true, true, true };

        [ObservableProperty]
        public partial bool IsRefreshing { get; set; }
        #endregion

        #region Constructor
        public MainPageViewModel(SongsService songsService, ArtistsService artistsService,
                                 AlbumsService albumsService, NowPlayingSyncService syncService)
        {
            _songsService = songsService;
            _bandsService = artistsService;
            _albumsService = albumsService;
            _syncService = syncService;

            // Registers collections in NowPlayingSyncService
            _syncService.Track(() => FeaturedSongs, () => RecentSongs, () => TrendingSongs);
        }
        #endregion

        #region Methods
        public async Task InitializeAsync() => await GetSongsAsync();
        #endregion

        #region Commands
        /// <summary>
        /// Gets all data from services and assigns it to ObservableCollections, and hides the placeholders.
        /// </summary>
        [RelayCommand]
        private async Task GetSongsAsync()
        {
            try
            {
                IsRefreshing = false;
                IsBusy = true;

                FeaturedSongs = [.. (await _songsService.GetFeaturedAsync(4)).Select(song => new Song(song)) ];
                ShowPlaceHolder[0] = false;

                RecentSongs = [.. (await _songsService.GetRecentAsync(15)).Select(song => new Song(song))];
                ShowPlaceHolder[1] = false;

                Albums = [.. await _albumsService.GetAllAsync(pageSize: 5)];
                ShowPlaceHolder[2] = false;

                TrendingSongs = [.. (await _songsService.GetTrendingAsync(5)).Select(song => new Song(song))];
                ShowPlaceHolder[3] = false;

                Artists = [.. await _bandsService.GetBandsAsync()];
                ShowPlaceHolder[4] = false;

                // Syncs the songs playbkack statuses after loading
                _syncService.SyncNow();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"====GetSongsAsync Error: {ex}");

                bool isAccepted = await Shell.Current.DisplayAlertAsync("Something went wrong...", "We coudn't load our data.", "Try again", "Cancel");
                
                if(isAccepted || ShowPlaceHolder[0])
                    await GetSongsAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Navigates to the song's details page
        /// </summary>
        /// <param name="song">The selected song</param>
        [RelayCommand]
        private async Task GoToSongDetailsAsync(SongDto song)
        {
            if (song is null || IsBusy)
                return;

            await Shell.Current.GoToAsync(nameof(PlayerPage), true,
                new Dictionary<string, object>
                {
                    {"Song", song }
                }
            );
        }

        /// <summary>
        /// Navigates to the album's details page
        /// </summary>
        /// <param name="albumId">The selected album</param>
        [RelayCommand]
        private async Task GoToAlbumDetailsAsync(int albumId)
        {
            await Shell.Current.GoToAsync($"{nameof(AlbumPage)}?AlbumId={albumId}");
        }
        #endregion
    }
}
