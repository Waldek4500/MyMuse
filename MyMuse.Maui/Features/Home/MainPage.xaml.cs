namespace MyMuse.Maui.Features.Home
{
    public partial class MainPage : ContentPage
    {
        private MainPageViewModel _mainPageViewModel;
        private bool _isInitalized;

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = _mainPageViewModel = mainPageViewModel;
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
