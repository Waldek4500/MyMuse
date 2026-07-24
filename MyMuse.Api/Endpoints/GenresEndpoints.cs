using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Data;
using MyMuse.Shared.Dtos;
using System.Runtime.CompilerServices;

namespace MyMuse.Api.Endpoints
{
    public static class GenresEndpoints
    {
        public static void MapGenresEndpoints(this WebApplication app)
        {
            RouteGroupBuilder group = app.MapGroup("/genres");

            group.MapGet("/", async (MyMuseContext dbContext)
                => await dbContext.Genres
                    .Select(genre => new GenreDto(
                        genre.Id,
                        genre.Name,
                        genre.AssociatedColor ?? String.Empty))
                    .AsNoTracking()
                    .ToListAsync()
            );
        }
    }
}
