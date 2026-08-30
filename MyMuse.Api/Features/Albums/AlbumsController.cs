using Microsoft.AspNetCore.Mvc;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Albums
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController(IAlbumRepository albumRepo) : ControllerBase
    {
        private readonly IAlbumRepository _albumRepo = albumRepo;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AlbumDto>), StatusCodes.Status200OK)]
        [EndpointSummary("Gets a full list of albums")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetAll([FromQuery] AlbumQuery query)
        {
            var albums = await _albumRepo.GetAllAsync(query);

            var albumsDtos = albums.Select(album => album.ToAlbumDto());

            return Ok(albumsDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AlbumDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [EndpointSummary("Get a album by its id")]
        public async Task<ActionResult<AlbumDto>> GetById([FromRoute] int id)
        {
            var album = await _albumRepo.GetByIdAsync(id);

            return album is null
                ? NotFound()
                : Ok(album.ToAlbumDto());
        }
    }
}
