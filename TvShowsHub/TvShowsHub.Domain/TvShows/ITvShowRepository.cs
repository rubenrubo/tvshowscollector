namespace TvShowsHub.Domain.TvShows;

public interface ITvShowRepository
{
    Task<TvShow?> GetTvShowsByIdAsync(int id);
    Task<TvShow> AddTvShowAsync(TvShow tvShow);
    Task UpdateTvShowAsync(TvShow tvShow);
    Task DeleteTvShowAsync(int id);
}