using Microsoft.Extensions.DependencyInjection;
using TvShowsHub.Domain.TvShows;
using TvShowsHub.TvMazeClient.TvShows;

namespace TvShowsHub.TvMazeClient.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddExternalClients(this IServiceCollection services)
    {
        services.AddScoped<ITvShowClient, TvShowsClient>();
    }
}