using Microsoft.EntityFrameworkCore;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Repository.Repositories;

public class TvShowRepository(TvShowsHubDbContext dbContext) : ITvShowRepository
{
    public async Task<TvShow?> GetTvShowsByIdAsync(int id)
    {
        return await dbContext.TvShows.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<TvShow> AddTvShowAsync(TvShow tvShow)
    {
        await dbContext.AddAsync(tvShow);
        await dbContext.SaveChangesAsync();
        return tvShow;
    }

    public async Task UpdateTvShowAsync(TvShow tvShow)
    {
        dbContext.Update(tvShow);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteTvShowAsync(int id)
    {
        var dbResult = await dbContext.TvShows.FirstOrDefaultAsync(x => x.Id == id);
        
        if (dbResult == null)
        {
            return;
        }
        
        dbContext.Remove(dbResult);
        await dbContext.SaveChangesAsync();
    }
}