using Microsoft.AspNetCore.Mvc;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.API.Controllers;

public class TvShowManagerController(IManageTvShowsService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult> Get(int page = 0, int pageSize = 500, string? name = null)
    {
        var result = await service.GetTvShowsAsync(page, pageSize, name);
        return Ok(result);
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