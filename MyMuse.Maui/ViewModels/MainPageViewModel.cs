using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Maui.Views;
using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;

namespace MyMuse.Maui.ViewModels
{
    public partial class MainPageViewModel : BaseViewModel
    {
        #region Private Members
        private readonly SongsService _songsService;
        #endregion

        #region Public Properties
        [ObservableProperty]
        public partial ObservableCollection<SongDto> FeaturedSongs { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<SongDto> RecentSongs { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<SongDto> TrendingSongs { get; set; } = [];

        [ObservableProperty]
        public partial ObservableCollection<BandDto> Bands { get; set; } = [];


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
                IsBusy = true;

                FeaturedSongs = [.. await _songsService.GetFeaturedSongsAsync()];
                RecentSongs = [.. await _songsService.GetRecentSongsAsync()];
                TrendingSongs = [.. await _songsService.GetTrendingSongsAsync()];
                Bands = [.. await _songsService.GetBandsAsync()];
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlertAsync("Error occurred", "We coudn't provide you songs.", "Try again");
                await GetSongsAsync();
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
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

            Debug.WriteLine($"==== {song.Title}");

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
