using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Entities;

namespace MyMuse.Api.Data
{
    public static class DataExtensions
    {
        public static void MigrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MyMuseContext>();
            
            dbContext.Database.Migrate();
        }

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

                        context.SaveChanges();
                    });
                }
            );
        }
    }
}
