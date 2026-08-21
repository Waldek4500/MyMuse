using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Songs
{
    public interface ISongRepository
    {
        public Task<List<Song>> GetAllAsync(SongQuery query);

        public Task<Song?> GetByIdAsync(int id);
    }
}
