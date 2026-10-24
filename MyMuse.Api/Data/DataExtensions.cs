using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Entities;

namespace MyMuse.Api.Data
{
    public static class DataExtensions
    {

        /// <summary>
        /// Automatically applies new migrations.
        /// </summary>
        public static void MigrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MyMuseContext>();
            
            dbContext.Database.Migrate();
        }


        /// <summary>
        /// Configures the database connection and registers database context in the application, including initial seeding.
        /// </summary>
        /// <param name="builder"></param>
        public static void AddMyMuseConnection(this WebApplicationBuilder builder)
        {
            var conString = builder.Configuration.GetConnectionString("MyMuse");

            builder.Services.AddDbContext<MyMuseContext>(
                options =>
                {
                    options.UseNpgsql(conString);

                    options.UseSeeding((context, _) =>
                    {
                        if (!context.Set<Role>().Any())
                        {
                            context.AddRange(
                                new Role { Name = "Vocalist" },
                                new Role { Name = "Guitarist" },
                                new Role { Name = "Bassist" },
                                new Role { Name = "Drummer" },
                                new Role { Name = "Keyboardist" }
                            );
                        }

                        if (!context.Set<Genre>().Any())
                        {
                            context.AddRange(
                                new Genre { Name = "Pop" },
                                new Genre { Name = "Rock" },
                                new Genre { Name = "Hip Hop" },
                                new Genre { Name = "Rap" },
                                new Genre { Name = "R&B" },
                                new Genre { Name = "Soul" },
                                new Genre { Name = "Jazz" },
                                new Genre { Name = "Blues" },
                                new Genre { Name = "Country" },
                                new Genre { Name = "Folk" },
                                new Genre { Name = "Metal" },
                                new Genre { Name = "Heavy Metal" },
                                new Genre { Name = "Hard Rock" },
                                new Genre { Name = "Punk" },
                                new Genre { Name = "Alternative" },
                                new Genre { Name = "Indie" },
                                new Genre { Name = "Indie Pop" },
                                new Genre { Name = "Grunge" },
                                new Genre { Name = "Electronic" },
                                new Genre { Name = "EDM" },
                                new Genre { Name = "House" },
                                new Genre { Name = "Techno" },
                                new Genre { Name = "Trance" },
                                new Genre { Name = "Dubstep" },
                                new Genre { Name = "Drum and Bass" },
                                new Genre { Name = "Future Bass" },
                                new Genre { Name = "Synthwave" },
                                new Genre { Name = "Ambient" },
                                new Genre { Name = "Classical" },
                                new Genre { Name = "Modern Classical" },
                                new Genre { Name = "Classical Piano" },
                                new Genre { Name = "Solo Piano" },
                                new Genre { Name = "Solo Guitar" },
                                new Genre { Name = "Solo Instruments" },
                                new Genre { Name = "Solo Classical Instruments" },
                                new Genre { Name = "Orchestral" },
                                new Genre { Name = "Soundtrack" },
                                new Genre { Name = "Video Games" }
                            );
                        }

                        context.SaveChanges();
                    });
                }
            );
        }
    }
}
