namespace MyMuse.Maui.Features.Search;

public partial class SearchPage : ContentPage
{
    private bool _isInitalized;

    private readonly SearchPageViewModel _searchPageViewModel;

	public SearchPage(SearchPageViewModel searchPageViewModel)
	{
		InitializeComponent();
		BindingContext = _searchPageViewModel = searchPageViewModel;
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

        if (_isInitalized)
            return;

        _isInitalized = true;
		await _searchPageViewModel.InitializeAsync();
    }
}