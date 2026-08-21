using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Data;
using MyMuse.Api.Entities;

namespace MyMuse.Api.Features.Genres
{
    public class GenreRepository(MyMuseContext dbContext) : IGenreRepository
    {
        private readonly MyMuseContext _dbContext = dbContext;

        public async Task<List<Genre>> GetAllAsync(GenreQuery query)
        {
            var genres = _dbContext.Genres.AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.OrderBy))
            {
                if (query.OrderBy.Equals("DisplayOrder", StringComparison.OrdinalIgnoreCase))
                    genres = query.IsDescending ? genres.OrderByDescending(genre => genre.DisplayOrder) : genres.OrderBy(genre => genre.DisplayOrder);
            }
            

            int skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await genres
                .Skip(skipNumber)
                .Take(query.PageSize)
                .ToListAsync();
        }
    }
}
