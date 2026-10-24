using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMuse.Maui.Features.About
{
    public partial class AboutPageViewModel : BaseViewModel
    {
        [RelayCommand]
        private static async Task GoToUrlAsync(string url)
        {
            await Browser.Default.OpenAsync(url);
        }
    }
}
