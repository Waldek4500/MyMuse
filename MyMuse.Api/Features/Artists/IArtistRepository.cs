using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Artists
{
    public interface IArtistRepository
    {
        public Task<List<Artist>> GetAllAsync(ArtistQuery query);
    }
}
