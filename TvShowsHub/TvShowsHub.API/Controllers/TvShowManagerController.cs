using Microsoft.AspNetCore.Mvc;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.API.Controllers;

public class TvShowManagerController(IManageTvShowsService service) : BaseController
{
    [HttpGet("sync")]
    public async Task<ActionResult> Sync()
    {
        await service.SyncTvMazeShowsAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] AddTvShowSpec tvShow)
    {
        var result = await service.AddTvShowAsync(tvShow);
        return Ok(result);
    }
    
    [HttpPut]
    public async Task<ActionResult> Put([FromBody] UpdateTvShowSpec tvShow)
    {
        await service.UpdateTvShowAsync(tvShow);
        return Ok();
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        await service.RemoveTvShowAsync(id);
        return Ok();
    }
}