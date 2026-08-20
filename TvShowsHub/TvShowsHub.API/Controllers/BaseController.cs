using Microsoft.AspNetCore.Mvc;

namespace TvShowsHub.API.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{ }