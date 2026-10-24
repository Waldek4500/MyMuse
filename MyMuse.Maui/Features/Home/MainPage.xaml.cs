using MyMuse.Maui.Views;

namespace MyMuse.Maui.Features.Home
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _mainPageViewModel;
        private bool _isInitalized;

        public MainPage(MainPageViewModel mainPageViewModel, PlayerEngineHost playerEngineHost)
        {
            InitializeComponent();
            BindingContext = _mainPageViewModel = mainPageViewModel;

            // Assigns MediaElement control
            PlayerEnginge.Content = playerEngineHost;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (_isInitalized)
                return;

            _isInitalized = true;

            await _mainPageViewModel.InitializeAsync();
        }
    }
}
