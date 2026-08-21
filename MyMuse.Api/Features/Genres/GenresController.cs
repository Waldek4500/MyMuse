using Microsoft.AspNetCore.Mvc;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController(IGenreRepository genreRepo) : ControllerBase
    {
        private readonly IGenreRepository _genreRepo = genreRepo;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GenreDto>), StatusCodes.Status200OK)]
        [EndpointSummary("Gets a full genres list")]
        [EndpointDescription("Returns all genres that matches the query params")]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll([FromQuery] GenreQuery query)
        {
            var genres = await _genreRepo.GetAllAsync(query);

            var genresDtos = genres.Select(genre => genre.ToGenreDto());

            return Ok(genresDtos);
        }
    }
}
