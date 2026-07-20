using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Data;
using MyMuse.Api.Dtos;
using MyMuse.Shared.Dtos;

namespace MyMuse.Api.Endpoints
{
    public static class SongsEndpoints
    {

        public static void MapSongsEndpoints(this WebApplication app)
        {
            RouteGroupBuilder group = app.MapGroup("/songs");

            group.MapGet("/", async (MyMuseContext dbContext)
                => await dbContext.Songs
                    .Select(song => new SongDto(
                        song.Id,
                        song.Title,
                        song.Album != null ? song.Album.Title : null,
                        song.Band!.Name,
                        song.BandId,
                        song.Lyrics,
                        song.AudioUrl,
                        song.Duration,
                        song.ReleaseDate,
                        song.IsSingle,
                        song.PlaysNumber,
                        song.ImageUrl ?? song.Album!.ImageUrl))
                    .AsNoTracking()
                    .ToListAsync()
            );

            group.MapGet("/band/{bandId}", async (int bandId, MyMuseContext dbContext) => {
                var songs = await dbContext.Songs
                    .Where(song => song.BandId == bandId)
                    .OrderBy(song => song.Id)
                    .Select(song => new SongDto(
                        song.Id,
                        song.Title,
                        song.Album != null ? song.Album.Title : null,
                        song.Band!.Name,
                        song.BandId,
                        song.Lyrics,
                        song.AudioUrl,
                        song.Duration,
                        song.ReleaseDate,
                        song.IsSingle,
                        song.PlaysNumber,
                        song.ImageUrl ?? song.Album!.ImageUrl))
                    .AsNoTracking()
                    .ToListAsync();

                return songs.Count == 0 ? Results.NotFound() : Results.Ok(songs);
            });

            group.MapGet("/album/{albumId}", async (int albumId, MyMuseContext dbContext) => {

                var songs = await dbContext.Songs
                    .Where(song => song.AlbumId == albumId)
                    .OrderBy(song => song.Id)
                    .Select(song => new SongDto(
                        song.Id,
                        song.Title,
                        song.Album != null ? song.Album.Title : null,
                        song.Band!.Name,
                        song.BandId,
                        song.Lyrics,
                        song.AudioUrl,
                        song.Duration,
                        song.ReleaseDate,
                        song.IsSingle,
                        song.PlaysNumber,
                        song.ImageUrl ?? song.Album!.ImageUrl))
                    .AsNoTracking()
                    .ToListAsync();

                return songs.Count == 0 ? Results.NotFound() : Results.Ok(songs);
            });

            group.MapGet("/featured/{count}", async (int count, MyMuseContext dbContext)
                => await dbContext.Songs
                    .OrderBy(song => EF.Functions.Random())
                    .Take(count)
                    .Select(song => new SongDto(
                        song.Id,
                        song.Title,
                        song.Album != null ? song.Album.Title : null,
                        song.Band!.Name,
                        song.BandId,
                        song.Lyrics,
                        song.AudioUrl,
                        song.Duration,
                        song.ReleaseDate,
                        song.IsSingle,
                        song.PlaysNumber,
                        song.ImageUrl ?? song.Album!.ImageUrl))
                    .AsNoTracking()
                    .ToListAsync()
            );

            group.MapGet("/trending/{count}", async (int count, MyMuseContext dbContext)
                => await dbContext.Songs
                    .OrderByDescending(song => song.PlaysNumber)
                    .Take(count)
                    .Select(song => new SongDto(
                        song.Id,
                        song.Title,
                        song.Album != null ? song.Album.Title : null,
                        song.Band!.Name,
                        song.BandId,
                        song.Lyrics,
                        song.AudioUrl,
                        song.Duration,
                        song.ReleaseDate,
                        song.IsSingle,
                        song.PlaysNumber,
                        song.ImageUrl ?? song.Album!.ImageUrl))
                    .AsNoTracking()
                    .ToListAsync()
            );

            group.MapGet("/bandRandom/{bandId}/{count}", async (int bandId, int count, MyMuseContext dbContext)
                => await dbContext.Songs
                    .Where(song => song.BandId == bandId)
                    .OrderBy(song => EF.Functions.Random())
                    .Take(count)
                    .Select(song => new SongDto(
                        song.Id,
                        song.Title,
                        song.Album != null ? song.Album.Title : null,
                        song.Band!.Name,
                        song.BandId,
                        song.Lyrics,
                        song.AudioUrl,
                        song.Duration,
                        song.ReleaseDate,
                        song.IsSingle,
                        song.PlaysNumber,
                        song.ImageUrl ?? song.Album!.ImageUrl))
                    .AsNoTracking()
                    .ToListAsync()
            );
        }
    }
}
