namespace TvShowsHub.Domain.TvShows;

public interface ISyncExternalTvShowsService
{
    Task SyncTvMazeShowsAsync();
}