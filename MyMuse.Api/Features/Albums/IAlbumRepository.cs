using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Albums
{
    public interface IAlbumRepository
    {
        public Task<List<Album>> GetAllAsync(AlbumQuery query);

        public Task<Album?> GetByIdAsync(int id);
    }
}
