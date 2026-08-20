using Microsoft.AspNetCore.Mvc;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.API.Controllers;

public class TvShowManagerController(IManageTvShowsService service) : BaseController
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var shows = await service.GetTvShowsAsync();
        return Ok(shows);
    }

    [HttpPost]
    public async Task<ActionResult> Post(TvShow tvShow)
    {
        var result = await service.AddTvShowAsync(tvShow);
        return Ok(result);
    }
}