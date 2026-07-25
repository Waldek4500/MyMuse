using CommunityToolkit.Mvvm.Input;
using MyMuse.Maui.Services;
using MyMuse.Maui.ViewModels;
using MyMuse.Maui.ViewModels.Base;
using MyMuse.Shared.Dtos;

namespace MyMuse.Maui.Features.Player
{
    public partial class PlayerPageViewModel : BaseViewModel, IQueryAttributable
    {
        public MusicPlayerViewModel Player { get; }
        public PlaybackQueueService Queue { get; }

        public PlayerPageViewModel(MusicPlayerViewModel player, PlaybackQueueService queue)
        {
            Player = player;
            Queue = queue;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (!query.TryGetValue("Song", out var song) || song is not SongDto)
                return;

            Player.CurrentSong = (SongDto)query["Song"];
        }
    }
}
