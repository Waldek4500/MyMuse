using MyMuse.Api.Entities;
using MyMuse.Shared.Dtos;
using MyMuse.Shared.Enums;

namespace MyMuse.Api.Features.Search
{
    public static class SearchMapper
    {
        public static SearchResultDto ToSearchResultDto(this SearchResultItem searchResult)
            => new(
                searchResult.Id,
                Enum.Parse<SearchResultType>(searchResult.Type, true),
                searchResult.Title,
                searchResult.Subtitle,
                searchResult.ImageUrl
            );
    }
}
