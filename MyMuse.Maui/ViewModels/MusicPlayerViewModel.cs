using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Pages;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MyMuse.Maui.ViewModels
{
    public partial class MusicPlayerViewModel : BaseViewModel, IQueryAttributable
    {
        #region Public Properties
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasTrack))]
        public partial SongDto CurrentSong { get; set; } = new(-1, "", null, "", 0, null, "", 0, new(1, 1, 1), false, 0, "");

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPlaying))]
        [NotifyPropertyChangedFor(nameof(PlayPauseBtnImage))]
        public partial MediaElementState State { get; set; } = MediaElementState.None;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PositionProgress))]
        public partial double PositionSeconds { get; set; }

        [ObservableProperty]
        public partial string? PositionText { get; set; }

        [ObservableProperty]
        public partial double DurationSeconds { get; set; }

        [ObservableProperty]
        public partial string? DurationText { get; set; }

        public double PositionProgress => DurationSeconds != 0 ? (PositionSeconds / DurationSeconds) : 0;

        [ObservableProperty]
        public partial bool IsBuffering { get; set; }

        public bool IsPlaying => State == MediaElementState.Playing;

        public bool HasTrack => CurrentSong is not null && !String.IsNullOrEmpty(CurrentSong.AudioUrl);

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(PlayInLoopBtnColor))]
        public partial bool ShouldPlayInLoop { get; set; }

        public Color PlayInLoopBtnColor => ShouldPlayInLoop ? Color.FromRgba("#964DDBFF") : Colors.White;

        public string PlayPauseBtnImage => IsPlaying ? "Icons/pause_icon.png" : "Icons/play_icon.png";

        // Queue
        public Queue<SongDto> SongsQueue { get; set; } = [];

        public Stack<SongDto> PreviousSongs { get; set; } = [];
        #endregion

        #region Private Fields
        private bool _isNavigating;
        private readonly SongsService _songsService;
        #endregion

        public MusicPlayerViewModel(SongsService songsService)
        {
            _songsService = songsService;
        }


        partial void OnStateChanged(MediaElementState oldValue, MediaElementState newValue)
        {
            Debug.WriteLine($"==== State: {newValue}");
        }
        #region Private Methods
        async partial void OnCurrentSongChanged(SongDto oldValue, SongDto newValue)
        {
            if (oldValue is not null && newValue.Id == oldValue.Id)
                return;

            SongsQueue.Clear();
            await FillQueueAsync();
        }

        /// <summary>
        /// Sets a new value of State property when MediaElement's state changed
        /// </summary>
        /// <param name="value">New MediaElement's state</param>
        partial void OnStateChanged(MediaElementState value)
        {
            IsBuffering = value is MediaElementState.Opening or MediaElementState.Buffering;
            PlayPauseCommand.NotifyCanExecuteChanged();
        }
        #endregion

        #region Public Methods
        public void GoToNextSongWhenMusicOver()
        {
            if (ShouldPlayInLoop)
                return;

            NextSong();
        }

        public async Task FillQueueAsync()
        {
            try
            {
                List<SongDto> songs = await _songsService.GetRandomSongsFromBandAsync(CurrentSong.BandId, 10);
                songs.Remove(CurrentSong);

                foreach (SongDto song in songs)
                    SongsQueue.Enqueue(song);
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"===Error in FillQueueAsync: {ex}");
            }
            
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
            if (!query.TryGetValue("Song", out var song) || song is not SongDto)
                return;

            CurrentSong = (SongDto)query["Song"];
        }
        #endregion

        #region Events
        public event EventHandler? PlayRequested;
        public event EventHandler? PauseRequested;
        public event EventHandler<TimeSpan>? SliderSeekCompleted;
        #endregion

        #region Commands
        [RelayCommand]
        private void NextSong()
        {
            if (SongsQueue.TryDequeue(out SongDto? song))
            {
                PreviousSongs.Push(CurrentSong);
                CurrentSong = song;
            }
        }

        [RelayCommand]
        private void PreviousSong()
        {
            if(PreviousSongs.TryPop(out SongDto? song))
            {
                SongsQueue.Enqueue(CurrentSong);
                CurrentSong = song;
            }
        }

        [RelayCommand]
        private void TogglePlayInLoop() => ShouldPlayInLoop = !ShouldPlayInLoop;

        /// <summary>
        /// Plays or pauses the song
        /// </summary>
        [RelayCommand]
        private void PlayPause()
        {
            if (IsPlaying)
                PauseRequested?.Invoke(this, EventArgs.Empty);
            else
                PlayRequested?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Navigates to previous page
        /// </summary>
        [RelayCommand]
        private async Task GoBackAsync()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        [RelayCommand]
        private async Task GoToDetailsAsync()
        {
            if (_isNavigating)
                return;

            _isNavigating = true;

            try
            {
                await Shell.Current.GoToAsync(nameof(SongDetailsPage));
            }
            finally
            {
                _isNavigating = false;
            }    
        }
        #endregion

        #region Helpers
        /// <summary>
        /// Changes TimeSpan to string with correct format
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        private static string FormatTime(TimeSpan time) =>
            time.Hours > 0
                ? time.ToString(@"hh\:mm\:ss")
                : time.ToString(@"mm\:ss");
        #endregion
    }
}
