using System.Diagnostics;

namespace MyMuse.Maui.Features.SearchResults;

public partial class SearchResultsPage : ContentPage
{

	public SearchResultsPage(SearchResultsPageViewModel searchResultsPageViewModel)
	{
		InitializeComponent();
        BindingContext = searchResultsPageViewModel;

        searchResultsPageViewModel.UnfocusSearchBar += () => SearchBar.Unfocus();
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (args.PreviousPage.ClassId != "SearchPage")
            return;

        base.OnAppearing();

        bool isFocused = SearchBar.Focus();

        if (!isFocused)
        {
            await Task.Delay(500);
            SearchBar.Focus();
        }
    }
}