using MyMuse.Maui.ViewModels;
using MyMuse.Shared.Dtos;
using System.Collections.ObjectModel;

namespace MyMuse.Maui
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _mainPageViewModel;

        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            BindingContext = _mainPageViewModel = mainPageViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _mainPageViewModel.InitializeAsync();
        }
    }
}
