namespace MyMuse.Api.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string AssociatedColor { get; set; } = "#7C3AED";
        public string? ImageUrl { get; set; }
    }
}
