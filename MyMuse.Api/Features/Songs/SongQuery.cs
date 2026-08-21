using MyMuse.Api.Models;

namespace MyMuse.Api.Features.Songs
{
    public class SongQuery : QueryObject
    {
        public int? ArtistId {  get; set; }
        public int? AlbumId { get; set; }
        public string? OrderBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
