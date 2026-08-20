namespace TvShowsHub.Domain.TvShows;

public interface ITvShowRepository
{
    Task<IEnumerable<TvShow>> GetTvShowsAsync(int page = 0, int pageSize = 500, string? name = null);
    Task<TvShow?> GetTvShowsByIdAsync(int id);
    Task AddTvShowsAsync(IEnumerable<TvShow> tvShows);
    Task<TvShow> AddTvShowAsync(TvShow tvShow);
    Task UpdateTvShowAsync(TvShow tvShow);
    Task DeleteTvShowAsync(int id);
}