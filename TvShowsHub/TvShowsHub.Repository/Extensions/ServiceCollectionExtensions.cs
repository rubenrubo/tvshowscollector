using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TvShowsHub.Domain.TvShows;
using TvShowsHub.Repository.Repositories;

namespace TvShowsHub.Repository.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TvShowsHubDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<ITvShowRepository, TvShowRepository>();
    }
}