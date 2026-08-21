using MyMuse.Api.Entities;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Artists
{
    public static class ArtistMapper
    {
        public static ArtistDto ToArtistDto(this Artist artist)
            => new(
                artist.Id,
                artist.Name,
                artist.FoundingDate,
                artist.Description,
                artist.ImageUrl
            );
    }
}
