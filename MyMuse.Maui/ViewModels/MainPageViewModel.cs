using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels.Base;
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
            await GetFeaturedSongsAsync();       
        }



        [RelayCommand]
        private async Task GetFeaturedSongsAsync()
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
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }
    }
}
