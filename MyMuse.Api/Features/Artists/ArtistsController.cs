using Microsoft.AspNetCore.Mvc;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Artists
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(IArtistRepository artistRepo) : ControllerBase
    {
        private readonly IArtistRepository _artistRepo = artistRepo;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ArtistDto>), StatusCodes.Status200OK)]
        [EndpointSummary("Gets a full artists list")]
        [EndpointDescription("Returns all artists that matches the query params")]
        public async Task<ActionResult<IEnumerable<ArtistDto>>> GetAll([FromQuery] ArtistQuery query)
        {
            var artists = await _artistRepo.GetAllAsync(query);

            var artistsDtos = artists.Select(artist => artist.ToArtistDto());

            return Ok(artistsDtos);
        }
    }
}
