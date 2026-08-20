namespace TvShowsHub.Domain.TvShows;

public interface IManageTvShowsService
{
    Task<TvShow[]> GetTvShowsAsync();
}