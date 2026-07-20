using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Pages;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Shared.Dtos;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyMuse.Maui.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        #region Private Members
        private readonly SongsService _songsService;
        #endregion

        #region Public Properties
        [ObservableProperty]
        public partial ObservableCollection<SongDto> FeaturedSongs { get; set; } = [.. new SongDto[4]];

        [ObservableProperty]
        public partial ObservableCollection<SongDto> RecentSongs { get; set; } = [.. new SongDto[4]];

        [ObservableProperty]
        public partial ObservableCollection<SongDto> TrendingSongs { get; set; } = [.. new SongDto[5]];

        [ObservableProperty]
        public partial ObservableCollection<BandDto> Bands { get; set; } = [.. new BandDto[4]];

        [ObservableProperty]
        public partial bool[] ShowPlaceHolder { get; set; } = { true, true, true, true };

        [ObservableProperty]
        public partial bool IsRefreshing { get; set; }
        #endregion

        #region Constructor
        public MainPageViewModel(SongsService songsService)
        {
            _songsService = songsService;
        }
        #endregion

        public async Task InitializeAsync()
        {
            await GetSongsAsync();       
        }

        #region Commands

        /// <summary>
        /// Gets all songs from service and assigns to ObservableCollections
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task GetSongsAsync()
        {
            try
            {
                IsRefreshing = false;
                IsBusy = true;

                FeaturedSongs = [.. await _songsService.GetFeaturedSongsAsync(4)];
                ShowPlaceHolder[0] = false;

                RecentSongs = [.. await _songsService.GetRecentSongsAsync()];
                ShowPlaceHolder[1] = false;

                TrendingSongs = [.. await _songsService.GetTrendingSongsAsync(5)];
                ShowPlaceHolder[2] = false;

                Bands = [.. await _songsService.GetBandsAsync()];
                ShowPlaceHolder[3] = false;
            }
            catch (Exception ex)
            {
                bool isAccepted = await Shell.Current.DisplayAlertAsync("Error occurred", "We coudn't provide you songs.", "Try again", "Cancel");
                
                if(isAccepted || ShowPlaceHolder[0])
                    await GetSongsAsync();

                Debug.WriteLine($"====GetSongsAsync Error: {ex}");
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

            await Shell.Current.GoToAsync(nameof(SongDetailsPage), true,
                new Dictionary<string, object>
                {
                    {"Song", song }
                }
            );
        }
        #endregion
    }
}
