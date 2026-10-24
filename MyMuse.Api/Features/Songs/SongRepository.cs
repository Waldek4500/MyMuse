using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Data;
using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Songs
{
    public class SongRepository(MyMuseContext dbContext) : ISongRepository
    {
        private readonly MyMuseContext _dbContext = dbContext;

        public async Task<List<Song>> GetAllAsync(SongQuery query)
        {
            var songs = _dbContext.Songs
                            .Include(s => s.Artist)
                            .Include(s => s.Album)
                            .AsQueryable();

            // Filtering by ArtistId or AlbumId
            if(query.ArtistId is not null)
                songs = songs.Where(song => song.ArtistId == query.ArtistId);

            if(query.AlbumId is not null)
                songs = songs.Where(song => song.AlbumId == query.AlbumId);

            // Sorting by PlaysNumber or randomly
            if (!String.IsNullOrWhiteSpace(query.OrderBy))
            {
                if (query.OrderBy.Equals("PlaysNumber", StringComparison.OrdinalIgnoreCase))
                    songs = query.IsDescending ? songs.OrderByDescending(song => song.PlaysNumber) : songs.OrderBy(song => song.PlaysNumber);

                else if (query.OrderBy.Equals("Random", StringComparison.OrdinalIgnoreCase))
                    songs = songs.OrderBy(song => EF.Functions.Random());
            }

            int skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await songs.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Song?> GetByIdAsync(int id)
        {
            return await _dbContext.Songs
                .Include(s => s.Artist)
                .Include(s => s.Album)
                .AsNoTracking()
                .FirstOrDefaultAsync(song => song.Id == id);
        }
    }
}
