using Microsoft.Extensions.DependencyInjection;
using TvShowsHub.Application.TvShows;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IManageTvShowsService, ManageTvShowsService>();
    }
}