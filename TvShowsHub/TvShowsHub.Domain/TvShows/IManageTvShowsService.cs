namespace TvShowsHub.Domain.TvShows;

public interface IManageTvShowsService
{
    Task <IEnumerable<TvShow>> GetTvShowsAsync(int page = 0, int pageSize = 500);
    Task<TvShow> AddTvShowAsync(AddTvShowSpec spec);
    Task UpdateTvShowAsync(UpdateTvShowSpec spec);
    Task RemoveTvShowAsync(int id);
}