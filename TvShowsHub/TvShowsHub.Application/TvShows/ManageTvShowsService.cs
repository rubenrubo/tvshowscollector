using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Application.TvShows;

public class ManageTvShowsService(
    ITvShowRepository repository) : IManageTvShowsService
{
    public async Task<IEnumerable<TvShow>> GetTvShowsAsync(int page = 0, int pageSize = 500)
    {
        return await repository.GetTvShowsAsync(page, pageSize);
    }

    public async Task<TvShow> AddTvShowAsync(AddTvShowSpec spec)
    {
        var show = new TvShow(spec);
        var result = await repository.AddTvShowAsync(show);
        return result;
    }

    public async Task UpdateTvShowAsync(UpdateTvShowSpec spec)
    {
        var existingTvShow = await repository.GetTvShowsByIdAsync(spec.Id);
        
        if (existingTvShow == null)
        {
            throw new Exception($"TvShow with id {spec.Id} not found");
        }
        
        existingTvShow.Update(spec);
        await repository.UpdateTvShowAsync(existingTvShow);
    }

    public async Task RemoveTvShowAsync(int id)
    {
        await repository.DeleteTvShowAsync(id);
    }
}