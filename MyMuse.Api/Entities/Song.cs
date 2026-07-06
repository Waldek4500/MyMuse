namespace MyMuse.Api.Entities;

public class Song
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int? AlbumId { get; set; }
    public Album? Album { get; set; }
    public int BandId { get; set; }
    public Band? Band { get; set; }
    public string? Lyrics { get; set; }
    public required string AudioUrl { get; set; }
    public int Duration { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public bool IsSingle { get; set; }
    public int PlaysNumber { get; set; }
}
