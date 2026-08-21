using MyMuse.Api.Models;

namespace MyMuse.Api.Features.Search
{
    public class SearchQuery : QueryObject
    {
        public string SearchFor { get; set; } = String.Empty;
    }
}
