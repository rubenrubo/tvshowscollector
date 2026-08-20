namespace TvShowsHub.Domain.TvShows;

public interface IManageTvShowsService
{
    Task SyncTvMazeShowsAsync();
    Task SyncTvMazeShowsAsync(int startPage);
    Task<TvShow> AddTvShowAsync(AddTvShowSpec spec);
    Task UpdateTvShowAsync(UpdateTvShowSpec spec);
    Task RemoveTvShowAsync(int id);
}