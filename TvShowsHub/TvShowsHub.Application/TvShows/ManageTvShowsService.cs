using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Application.TvShows;

public class ManageTvShowsService(ITvShowClient client) : IManageTvShowsService
{
    public async Task<TvShow[]> GetTvShowsAsync()
    {
        var shows = await client.GetTvShows();
        return shows;
    }
}