namespace MyMuse.Shared.Dtos;

public record SongDto(
    int Id,
    string Title,
    string? Album,
    string Artist,
    int ArtistId,
    string? Lyrics,
    string AudioUrl,
    int Duration,
    DateOnly ReleaseDate,
    bool IsSingle,
    int PlaysNumber,
    string? ImageUrl
);
