namespace MyMuse.Shared.Dtos
{
    public record BandDto(int Id, string Name, DateOnly FoundingDate, string? Description, string? ImageUrl);
}
