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

namespace MyMuse.Maui.Features.Search
{
    public partial class SearchPageViewModel : BaseViewModel
    {
        #region Private Members
        private readonly GenresService _genresService;
        #endregion

        #region Public Properties
        [ObservableProperty]
        public partial ObservableCollection<GenreDto> Genres {  get; set; }
        #endregion

        #region Constructor
        public SearchPageViewModel(GenresService genresService)
        {
            _genresService = genresService;
        }
        #endregion


        public async Task InitializeAsync() => await GetGenresAsync();

        [RelayCommand]
        private async Task GetGenresAsync()
        {
            try
            {
                IsBusy = true;

                Genres = [.. await _genresService.GetGenresAsync()];
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetGenresAsync: {ex}");
            }
            finally
            {
                IsBusy = false;
            }
        }

    }
}
