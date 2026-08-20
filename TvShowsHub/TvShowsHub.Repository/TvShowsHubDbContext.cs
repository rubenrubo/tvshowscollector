using Microsoft.EntityFrameworkCore;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Repository;

public class TvShowsHubDbContext(DbContextOptions<TvShowsHubDbContext> options) : DbContext(options)
{
    public DbSet<TvShow> TvShows { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<TvShow>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.HasIndex(e => e.TvMazeId).IsUnique();
        });
    }
}