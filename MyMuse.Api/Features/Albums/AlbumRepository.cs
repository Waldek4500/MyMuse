using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Data;
using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Albums;

public class AlbumRepository(MyMuseContext dbContext) : IAlbumRepository
{
    private readonly MyMuseContext _dbContext = dbContext;

    public async Task<List<Album>> GetAllAsync(AlbumQuery query)
    {
        var albums = _dbContext.Albums
            .Include(a => a.Artist)
            .AsNoTracking()
            .AsQueryable();

        int skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await albums.
            Skip(skipNumber).
            Take(query.PageSize).
            ToListAsync();
    }

    public async Task<Album?> GetByIdAsync(int id)
        => await _dbContext.Albums
            .Include(a => a.Artist)
            .AsNoTracking()
            .FirstOrDefaultAsync(album => album.Id == id);
}
