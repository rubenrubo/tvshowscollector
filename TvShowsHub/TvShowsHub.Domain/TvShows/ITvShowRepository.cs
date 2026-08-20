namespace TvShowsHub.Domain.TvShows;

public interface ITvShowRepository
{
    Task<TvShow[]> GetTvShowsAsync();
    Task<TvShow> AddTvShowAsync(TvShow tvShow);
    Task DeleteTvShowAsync(int id);
}