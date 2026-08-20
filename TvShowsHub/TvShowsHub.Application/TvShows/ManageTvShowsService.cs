using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Application.TvShows;

public class ManageTvShowsService(ITvShowClient client, ITvShowRepository repository) : IManageTvShowsService
{
    public async Task<TvShow[]> GetTvShowsAsync()
    {
        var shows = await client.GetTvShows();
        return shows;
    }

    public async Task<TvShow> AddTvShowAsync(TvShow tvShow)
    {
        var result = await repository.AddTvShowAsync(tvShow);
        return result;
    }
}