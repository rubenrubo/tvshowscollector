using Microsoft.EntityFrameworkCore;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Repository;

public class TvShowsHubDbContext(DbContextOptions<TvShowsHubDbContext> options) : DbContext(options)
{
    public DbSet<TvShow> TvShows { get; set; }
}