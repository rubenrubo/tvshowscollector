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
}