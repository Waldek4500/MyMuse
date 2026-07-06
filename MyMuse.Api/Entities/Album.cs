namespace MyMuse.Api.Entities;

public class Album
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int BandId { get; set; }
    public Band? Band { get; set; }
    public string? ImageUrl { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public string? Description { get; set; }
}