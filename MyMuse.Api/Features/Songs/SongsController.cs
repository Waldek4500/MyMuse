using Microsoft.AspNetCore.Mvc;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Songs
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController(ISongRepository songRepo) : ControllerBase
    {
        private readonly ISongRepository _songRepo = songRepo;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<SongDto>), StatusCodes.Status200OK)]
        [EndpointSummary("Gets a full songs list")]
        [EndpointDescription("Returns all songs that matches the query params")]
        public async Task<ActionResult<IEnumerable<SongDto>>> GetAll([FromQuery] SongQuery query)
        {
            var songs = await _songRepo.GetAllAsync(query);

            var songsDtos = songs.Select(song => song.ToSongDto());

            return Ok(songsDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(SongDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Get a song by its id")]
        public async Task<ActionResult<SongDto>> GetById([FromRoute] int id)
        {
            var song = await _songRepo.GetByIdAsync(id);

            return song is null ? NotFound() : Ok(song.ToSongDto()); 
        }
    }
}
