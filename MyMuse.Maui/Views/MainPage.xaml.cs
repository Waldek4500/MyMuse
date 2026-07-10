using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using MyMuse.Maui.ViewModels;
using MyMuse.Shared.Dtos;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MyMuse.Maui
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
            if (_isInitalized)
                return;

            _isInitalized = true;

            await _mainPageViewModel.InitializeAsync();
        }
    }
}
