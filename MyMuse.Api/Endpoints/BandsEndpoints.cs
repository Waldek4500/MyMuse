using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Data;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Endpoints
{
    public static class BandsEndpoints
    {

        public static void MapBandsEndpoints(this WebApplication app)
        {
            RouteGroupBuilder group = app.MapGroup("/bands");

            group.MapGet("/", async (MyMuseContext dbContext)
                => await dbContext.Bands
                        .Take(4)
                        .Select(band => new BandDto(
                            band.Id,
                            band.Name,
                            band.FoundingDate,
                            band.Description,
                            band.ImageUrl))
                        .AsNoTracking()
                        .ToListAsync()
            );
        }
    }
}
