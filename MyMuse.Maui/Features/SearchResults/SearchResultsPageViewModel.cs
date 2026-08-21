using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Features.Player;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Shared.Dtos;
using MyMuse.Shared.Enums;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyMuse.Maui.Features.SearchResults
{
    public partial class SearchResultsPageViewModel : BaseViewModel
    {
        #region Private Members
        private readonly SearchService _searchService;
        private readonly SongsService _songsService;

        private CancellationTokenSource? _searchCts;

        private int _resultsPageNumber = 1;
        private const int ResultsPageSize = 20;
        private const int SuggestionsPageSize = 10;

        private bool _isLoadingMore;
        private bool _hasMore = true;

        private List<SearchResultDto> _results = [];
        #endregion

        #region Public Properties
        [ObservableProperty]
        public partial SearchState SearchState { get; set; } = SearchState.Suggestions;

        [ObservableProperty]
        public partial SearchFilter SelectedFilter { get; set; } = SearchFilter.All;

        [ObservableProperty]
        public partial string SearchText { get; set; } = string.Empty;

        public List<SearchResultDto> Results
        {
            get => _results;
            set
            {
                _results = value;
                ApplyFilter();
            }
        }

        [ObservableProperty]
        public partial ObservableCollection<SearchResultDto> FilteredResults { get; set; } = [];
        #endregion

        #region Constructor
        public SearchResultsPageViewModel(SearchService searchService, SongsService songsService)
        {
            _searchService = searchService;
            _songsService = songsService;
        }
        #endregion

        public event Action? UnfocusSearchBar;

        #region Private Methods
        /// <summary>
        /// Invokes searching when user types new char, it inludes debounce
        /// </summary>
        /// <param name="value">The new search value</param>
        async partial void OnSearchTextChanged(string value)
        {
            _searchCts?.Cancel();

            if (string.IsNullOrWhiteSpace(value))
            {
                Results.Clear();
                return;
            }

            _searchCts = new CancellationTokenSource();

            Results = [.. await SearchAsync(value, SuggestionsPageSize, token: _searchCts.Token)];
        }

        /// <summary>
        /// Loads search results from API 
        /// </summary>
        /// <param name="value">The value to search for</param>
        /// <param name="pageSize">The number of results per page</param>
        /// <param name="pageNumber">The page number. Defaults to 1.</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>A list of search results</returns>
        private async Task<List<SearchResultDto>> SearchAsync(string value, int pageSize, int pageNumber = 1, CancellationToken token = default)
        {
            try
            {
                await Task.Delay(300, token);

                IsBusy = true;

                return await _searchService.SearchAsync(value, pageSize, pageNumber, token);     
            }
            catch (Exception ex)
            {
                if(ex is not OperationCanceledException)
                    Debug.WriteLine($"== SearchAsync Error: {ex}");

                return [];
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Filters results and assings its to <see cref="FilteredResults"/>
        /// </summary>
        private void ApplyFilter()
        {
            var filtered = SelectedFilter switch
            {
                SearchFilter.Songs => _results.Where(result => result.Type == SearchResultType.Song),
                SearchFilter.Albums => _results.Where(result => result.Type == SearchResultType.Album),
                SearchFilter.Artists => _results.Where(result => result.Type == SearchResultType.Artist),
                _ => _results
            };

            FilteredResults = [.. filtered];
        }

        partial void OnSelectedFilterChanged(SearchFilter value) => ApplyFilter();
        #endregion

        #region Commands
        /// <summary>
        /// Changes <see cref="SearchState"/> to Results and invokes loading results from API
        /// </summary>
        [RelayCommand]
        private async Task ShowResultsAsync()
        {
            _searchCts = new CancellationTokenSource();
            Results = [.. await SearchAsync(SearchText, ResultsPageSize, token: _searchCts.Token)];

            SearchState = SearchState.Results;
            UnfocusSearchBar?.Invoke();
        }

        /// <summary>
        /// Changes <see cref="SearchState"/> to Suggestions and preserves only certain number of results
        /// </summary>
        [RelayCommand]
        private void ShowSuggestions()
        {
            SelectedFilter = SearchFilter.All;
            Results = [.. Results.Take(SuggestionsPageSize)];
            SearchState = SearchState.Suggestions;
        }

        /// <summary>
        /// Load next page of results from API
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        private async Task LoadMoreAsync()
        {
            if (_isLoadingMore || !_hasMore)
                return;

            _isLoadingMore = true;

            _resultsPageNumber++;
            List<SearchResultDto> results = await SearchAsync(SearchText, ResultsPageSize, _resultsPageNumber);

            if(results.Count < ResultsPageSize)
                _hasMore = false;

            foreach (var result in results)
                _results.Add(result);

            ApplyFilter();

            _isLoadingMore = false;
        }

        /// <summary>
        /// Depending on what type of search result user has selected:
        /// 1. for song it loads song and navigates to player
        /// 2. for album it loads album and navigates to album page
        /// 3. for artist it loads artist and navigates to artist page
        /// </summary>
        /// <param name="searchResultDto">Selected search result</param>
        [RelayCommand]
        private async Task GoToSelectedAsync(SearchResultDto searchResultDto)
        {
            switch (searchResultDto.Type)
            {
                case SearchResultType.Song:
                    try
                    {
                        SongDto songDto = await _songsService.GetByIdAsync(searchResultDto.Id);

                        await Shell.Current.GoToAsync(nameof(PlayerPage), true,
                            new Dictionary<string, object>
                            {
                                {"Song", songDto}
                            });
                    } 
                    catch(Exception ex)
                    {
                        Debug.WriteLine($"Error in GoToSelectedAsync: {ex}");
                    }
                    break;
            }
        }
        #endregion
    }
}
