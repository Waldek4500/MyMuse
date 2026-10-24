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
using CommunityToolkit.Mvvm.Collections;
using MyMuse.Maui.Features.SearchResults;

namespace MyMuse.Maui.Features.Search
{
    public partial class SearchPageViewModel : BaseViewModel
    {
        #region Private Members
        private readonly GenresService _genresService;

        private int _pageNumber = 1;
        private const int PageSize = 6;
        #endregion

        #region Public Properties
        [ObservableProperty]
        public partial ObservableCollection<GenreDto> Genres { get; set; } = [];

        [ObservableProperty]
        public partial bool CanExpand { get; set; } = true;
        #endregion

        #region Constructor
        public SearchPageViewModel(GenresService genresService)
        {
            _genresService = genresService;
        }
        #endregion


        public async Task InitializeAsync() => await GetGenresAsync();

        /// <summary>
        /// Gets specific page of genres from the API 
        /// </summary>
        private async Task GetGenresAsync()
        {
            try
            {
                IsBusy = true;

                List<GenreDto> genres = [.. await _genresService.GetGenresAsync(PageSize, _pageNumber)];

                if (genres.Count < PageSize || genres.Count == 0)
                {
                    CanExpand = false;
                    return;
                }

                foreach (var genre in genres)
                    Genres.Add(genre);
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

        [RelayCommand]
        private async Task ShowMore()
        {
            _pageNumber += 1;
            await GetGenresAsync();
        }

        [RelayCommand]
        private async Task GoToSearchResultsAsync()
        {
            await Shell.Current.GoToAsync(nameof(SearchResultsPage));
        }
    }
}
