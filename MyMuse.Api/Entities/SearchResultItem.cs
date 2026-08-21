namespace MyMuse.Api.Entities
{
    public class SearchResultItem
    {
        public int Id { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? ImageUrl { get; set; }
        public float Score { get; set; }
    }
}
