using MyMuse.Api.Entities;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Songs
{
    public static class SongMapper
    {
        public static SongDto ToSongDto(this Song song)
            => new(
                song.Id,
                song.Title,
                song.Album?.Title,
                song.Artist!.Name,
                song.ArtistId,
                song.Lyrics,
                song.AudioUrl,
                song.Duration,
                song.ReleaseDate,
                song.IsSingle,
                song.PlaysNumber,
                song.ImageUrl ?? song.Album?.ImageUrl
            );
    }
}
