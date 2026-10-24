using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Shared.Dtos;

namespace MyMuse.Maui.Features.Player
{
    public partial class PlayerPageViewModel(MusicPlayerViewModel player, PlaybackQueueService queue) : BaseViewModel, IQueryAttributable
    {
        public MusicPlayerViewModel Player { get; } = player;
        public PlaybackQueueService Queue { get; } = queue;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (!query.TryGetValue("Song", out var song) || song is not SongDto)
                return;

            Player.CurrentSong = (SongDto)query["Song"];
         }

        [RelayCommand]
        private void Pause() => Player.Pause();

        [RelayCommand]
        private void SeekToAndResume(double seconds) => Player.SeekToAndResume(seconds);
    }
}
