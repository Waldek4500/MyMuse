namespace MyMuse.Shared.Dtos
{
    public record ArtistDto(int Id, string Name, DateOnly? FoundingDate, string? Description, string? ImageUrl);
}
