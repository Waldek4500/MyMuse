using MyMuse.Maui.Services;
using MyMuse.Maui.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMuse.Maui.Pages
{
    public abstract class MusicAwarePage : ContentPage
    {
        private readonly MiniPlayerBar _playerBar;

        protected MusicAwarePage(MiniPlayerBar miniPlayerBar) => _playerBar = miniPlayerBar;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //if (this.FindByName<ContentView>("MiniPlayerSlot") is { } slot)
            //{
            //    if (_playerBar.Parent is ContentView previousSlot)
            //        previousSlot.Content = null;

            //     slot.Content = _playerBar;
            //}
        }
    }
}
