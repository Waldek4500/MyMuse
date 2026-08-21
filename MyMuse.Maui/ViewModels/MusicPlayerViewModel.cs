using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Shared.Dtos;

namespace MyMuse.Maui.ViewModels
{
    public partial class MusicPlayerViewModel : BaseViewModel
    {
        #region Public Properties
        [ObservableProperty]
        public partial SongDto? CurrentSong { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(HasTrack))]
        public partial string? AudioUrl { get; set; }

        [ObservableProperty]
        public partial string? SongTitle { get; set; }

        [ObservableProperty]
        public partial string? Artist { get; set; }

        [ObservableProperty]
        public partial string? ArtworkUrl { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPlaying))]
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

        public bool HasTrack => AudioUrl is not null && !String.IsNullOrEmpty(AudioUrl);

        [ObservableProperty]
        public partial bool ShouldPlayInLoop { get; set; }
        #endregion

        #region Private Members
        private readonly SongsService _songsService;
        #endregion

        public MusicPlayerViewModel(SongsService songsService)
        {
            _songsService = songsService;
        }

        #region Private Methods
        async partial void OnCurrentSongChanged(SongDto? oldValue, SongDto? newValue)
        {
            if (newValue is null || newValue.Id == oldValue?.Id)
                return;

            SongTitle = newValue.Title;
            Artist = newValue.Artist;
            ArtworkUrl = newValue.ImageUrl ?? "";
            AudioUrl = newValue.AudioUrl;

            SongChanged?.Invoke(this, newValue);
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
        /// When user starts dragging slider, stops song
        /// </summary>
        public void BeginUserSeek() => PauseRequested?.Invoke(this, EventArgs.Empty);

        /// <summary>
        /// When user complete dragging slider, resume song and seek to new positon
        /// </summary>
        /// <param name="seconds"></param>
        public void CompleteUserSeek(double seconds) =>
            SliderSeekCompleted?.Invoke(this, TimeSpan.FromSeconds(seconds));
        #endregion

        #region Events
        public event EventHandler<SongDto>? SongChanged;

        public event EventHandler? PlayRequested;
        public event EventHandler? PauseRequested;
        public event EventHandler<TimeSpan>? SliderSeekCompleted;
        #endregion

        #region Commands
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