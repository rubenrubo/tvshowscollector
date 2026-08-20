namespace TvShowsHub.Domain.TvShows;

public interface ITvShowRepository
{
    Task<TvShow?> GetTvShowsByIdAsync(int id);
    Task AddTvShowsAsync(IEnumerable<TvShow> tvShows);
    Task<TvShow> AddTvShowAsync(TvShow tvShow);
    Task UpdateTvShowAsync(TvShow tvShow);
    Task DeleteTvShowAsync(int id);
}