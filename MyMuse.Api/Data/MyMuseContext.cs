using Microsoft.EntityFrameworkCore;
using MyMuse.Api.Entities;

namespace MyMuse.Api.Data;

public class MyMuseContext(DbContextOptions<MyMuseContext> options) : DbContext(options)
{
    public DbSet<Artist> Artists => Set<Artist>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<BandMember> BandMembers => Set<BandMember>();
    public DbSet<Album> Albums => Set<Album>();
    public DbSet<Song> Songs => Set<Song>();
    public DbSet<Genre> Genres => Set<Genre>();
    public DbSet<SongGenre> SongGenres => Set<SongGenre>();

    public DbSet<SearchResultItem> SearchResults => Set<SearchResultItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresExtension("pg_trgm");

        modelBuilder.Entity<SearchResultItem>().HasNoKey().ToView(null);

        modelBuilder.Entity<Genre>()
            .Property(g => g.AssociatedColor)
            .HasDefaultValue("#7C3AED");

        modelBuilder.Entity<Genre>()
            .Property(g => g.IsFeatured)
            .HasDefaultValue(true);    
    }
}
