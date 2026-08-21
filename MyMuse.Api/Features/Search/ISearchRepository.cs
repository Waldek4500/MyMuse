using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Search
{
    public interface ISearchRepository
    {
        public Task<List<SearchResultItem>> SearchAsync(SearchQuery query, CancellationToken ct);
    }
}
