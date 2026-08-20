namespace TvShowsHub.Domain.TvShows;

public interface IManageTvShowsService
{
    Task<TvShow[]> GetTvShowsAsync();
    Task<TvShow> AddTvShowAsync(AddTvShowSpec spec);
    Task UpdateTvShowAsync(UpdateTvShowSpec spec);
    Task RemoveTvShowAsync(int id);
}