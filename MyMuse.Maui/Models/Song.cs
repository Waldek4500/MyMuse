using CommunityToolkit.Mvvm.ComponentModel;
using MyMuse.Maui.ViewModels;
using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMuse.Maui.Models
{
    public partial class Song : ObservableObject
    {
        public SongDto Data { get; }

        [ObservableProperty]
        public partial bool IsSelected { get; set; } = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPaused))]
        public partial bool IsPlaying { get; set; }

        public bool IsPaused => !IsPlaying;

        public Song(SongDto song)
        {
            Data = song;
        }
    }
}
