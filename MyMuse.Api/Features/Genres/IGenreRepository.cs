using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Genres
{
    public interface IGenreRepository
    {
        public Task<List<Genre>> GetAllAsync(GenreQuery query);
    }
}
