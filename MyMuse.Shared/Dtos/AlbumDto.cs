namespace MyMuse.Shared.Dtos;

public record AlbumDto
(
    int Id,
    string Title,
    string Artist,
    int ArtistId,
    string ImageUrl,
    DateOnly ReleaseDate,
    string? Description
);
