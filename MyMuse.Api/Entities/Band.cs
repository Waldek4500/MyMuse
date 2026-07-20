namespace MyMuse.Api.Entities;

public class Band
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateOnly FoundingDate { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
}
