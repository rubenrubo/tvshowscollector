using Microsoft.Extensions.DependencyInjection;
using TvShowsHub.Application.Common;
using TvShowsHub.Application.TvShows;
using TvShowsHub.Domain.Common;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IManageTvShowsService, ManageTvShowsService>();
        services.AddSingleton<IRateLimiter>(new RateLimiter(maxCalls: 20, timeWindow: TimeSpan.FromSeconds(10)));
    }
}