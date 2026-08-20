namespace TvShowsHub.Domain.TvShows;

public interface ITvShowClient
{
    Task<TvShow[]> GetTvShows();
}