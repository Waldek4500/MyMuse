using CommunityToolkit.Mvvm.ComponentModel;
using MyMuse.Maui.ViewModels;
using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMuse.Maui.Models
{
    public partial class Song(SongDto song) : ObservableObject
    {
        public SongDto Data { get; } = song;

        [ObservableProperty]
        public partial bool IsSelected { get; set; } = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPaused))]
        public partial bool IsPlaying { get; set; }

        public bool IsPaused => !IsPlaying;

        /// <summary>
        /// For numbering purposes when the track is part of an album or sequence
        /// </summary>
        public int Number { get; set; }

        public string DurationText => TimeSpan.FromSeconds(Data.Duration).ToString(@"mm\:ss");
    }
}
