using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;

namespace MyMuse.Maui.ViewModels
{
    public partial class SongDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {
        #region Public Properties
        [ObservableProperty]
        public partial SongDto Song { get; set; } = new(0, "", null, "", null, "", 0, new(1, 1, 1), false, 0, "");

        [ObservableProperty]
        public partial MediaElementState State { get; set; } = MediaElementState.None;

        [ObservableProperty]
        public partial double PositionSeconds { get; set; }

        [ObservableProperty]
        public partial string PositionText { get; set; }

        [ObservableProperty]
        public partial double DurationSeconds { get; set; }

        [ObservableProperty]
        public partial string DurationText { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPlaying))]
        public partial bool IsBuffering { get; set; }

        public bool IsPlaying => State == MediaElementState.Playing;

        public string PlayPauseBtnImage => IsPlaying ? "Icons/pause_icon.png" : "Icons/play_icon.png";
        #endregion


        #region Methods

        /// <summary>
        /// Sets new value of State property when MediaElement's state changed
        /// </summary>
        /// <param name="value">New MediaElement's state</param>
        partial void OnStateChanged(MediaElementState value)
        {
            IsBuffering = value is MediaElementState.Opening or MediaElementState.Buffering;
            OnPropertyChanged(nameof(PlayPauseBtnImage));
            PlayPauseCommand.NotifyCanExecuteChanged();
        }

        /// <summary>
        /// Updates the current postion of song
        /// </summary>
        /// <param name="postion">New song position</param>
        public void UpdatePostion(TimeSpan postion)
        {
            PositionSeconds = postion.TotalSeconds;
            PositionText = FormatTime(postion);
        }

        /// <summary>
        /// Updates the song duration
        /// </summary>
        /// <param name="duration">Song duration</param>
        public void UpdateDuration(TimeSpan duration)
        {
            DurationSeconds = duration.TotalSeconds;
            DurationText = FormatTime(duration);
        }

        /// <summary>
        /// When user starts dragging slider stops song
        /// </summary>
        public void BeginUserSeek() => PauseRequested?.Invoke(this, EventArgs.Empty);


        /// <summary>
        /// When user complete dragging slider resume song and seek to new positon
        /// </summary>
        /// <param name="seconds"></param>
        public void CompleteUserSeek(double seconds) =>
            SliderSeekCompleted?.Invoke(this, TimeSpan.FromSeconds(seconds));

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Song = (SongDto)query["Song"];
        }
        #endregion

        #region Public Events
        public event EventHandler? PlayRequested;
        public event EventHandler? PauseRequested;
        public event EventHandler<TimeSpan>? SliderSeekCompleted;
        #endregion

        /// <summary>
        /// Navigates to previous page
        /// </summary>
        #region Commands
        [RelayCommand]
        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..", true);
        }


        /// <summary>
        /// Plays or pauses the song
        /// </summary>
        [RelayCommand]
        private void PlayPause()
        {
            if(IsPlaying)
                PauseRequested?.Invoke(this, EventArgs.Empty);
            else
                PlayRequested?.Invoke(this, EventArgs.Empty);
        }

        #endregion


        /// <summary>
        /// Changes TimeSpan to string with correct format
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        #region Helpers
        private static string FormatTime(TimeSpan time) =>
            time.Hours > 0
                ? time.ToString(@"hh\:mm\:ss")
                : time.ToString(@"mm\:ss");
        #endregion
    }
}
