using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Data;
using MyMuse.Api.Entities;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Features.Artists
{
    public class ArtistRepository(MyMuseContext dbContext) : IArtistRepository
    {
        private readonly MyMuseContext _dbContext = dbContext;

        public async Task<List<Artist>> GetAllAsync(ArtistQuery query)
        {
            var artists = _dbContext.Artists.AsQueryable();

            int skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await artists.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }
    }
}
