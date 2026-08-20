using Microsoft.AspNetCore.Mvc;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.API.Controllers;

public class TvMazeSyncController(ISyncExternalTvShowsService service) : BaseController
{
    [HttpGet("sync")]
    public async Task<ActionResult> Sync()
    {
        await service.SyncTvMazeShowsAsync();
        return Ok();
    }
}