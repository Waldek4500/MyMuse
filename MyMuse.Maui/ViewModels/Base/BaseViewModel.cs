using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMuse.Maui.ViewModels.Base
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial string Title { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        public partial bool IsBusy { get; set; }

        public bool IsNotBusy => !IsBusy;

        /// <summary>
        /// Navigates to previous page
        /// </summary>
        [RelayCommand]
        private static async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..", true);
        }
    }
}
