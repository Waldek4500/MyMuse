using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Entities;

namespace MyMuse.Api.Data;

public class MyMuseContext(DbContextOptions<MyMuseContext> options) : DbContext(options)
{
    public DbSet<Band> Bands => Set<Band>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<BandMember> BandMembers => Set<BandMember>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<SongGenre> SongGenres => Set<SongGenre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Genre>()
            .Property(g => g.AssociatedColor)
            .HasDefaultValue("#7C3AED");
    }
}
