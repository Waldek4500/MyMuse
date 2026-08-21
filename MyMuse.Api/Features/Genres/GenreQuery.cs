using MyMuse.Api.Models;

namespace MyMuse.Api.Features.Genres
{
    public class GenreQuery : QueryObject
    {
        public string? OrderBy { get; set; }
        public bool IsDescending { get; set; }
    }
}
