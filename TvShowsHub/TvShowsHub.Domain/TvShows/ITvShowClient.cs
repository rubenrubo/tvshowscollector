namespace TvShowsHub.Domain.TvShows;

public interface ITvShowClient
{
    Task<TvShow[]> GetTvShowsAsync(int page);
}