using MyMuse.Api.Entities;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Albums
{
    public static class AlbumMapper
    {
        public static AlbumDto ToAlbumDto(this Album album)
            => new(
                album.Id,
                album.Title,
                album.Artist!.Name,
                album.ArtistId,
                album.ImageUrl ?? "",
                album.ReleaseDate,
                album.Description
                );
    }
}
