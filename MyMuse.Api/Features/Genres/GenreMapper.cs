using MyMuse.Api.Entities;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Genres
{
    public static class GenreMapper
    {
        public static GenreDto ToGenreDto(this Genre genre)
            => new(
                genre.Id,
                genre.Name,
                genre.AssociatedColor,
                genre.ImageUrl
            );
    }
}
