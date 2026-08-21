namespace MyMuse.Api.Models
{
    public abstract class QueryObject
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}
