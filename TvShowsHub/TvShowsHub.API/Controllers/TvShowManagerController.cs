using Microsoft.AspNetCore.Mvc;

namespace TvShowsHub.API.Controllers;

public class TvShowManagerController : BaseController
{
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var result = "Hello from TvShowManagerController!";
        return Ok(result);
    }
}