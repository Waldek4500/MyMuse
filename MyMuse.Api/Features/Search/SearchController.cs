using Microsoft.AspNetCore.Mvc;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Search
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController(ISearchRepository searchRepo) : ControllerBase
    {
        private readonly ISearchRepository _searchRepo = searchRepo;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SearchResultDto>), StatusCodes.Status200OK)]
        [EndpointSummary("Search for items similar to query")]
        public async Task<ActionResult<IEnumerable<SearchResultDto>>> Search([FromQuery] SearchQuery query, CancellationToken ct = default)
        {
            var results = await _searchRepo.SearchAsync(query, ct);

            var resultsDtos = results.Select(result => result.ToSearchResultDto());

            return Ok(resultsDtos);
        }
    }
}
