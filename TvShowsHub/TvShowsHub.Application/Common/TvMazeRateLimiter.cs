using TvShowsHub.Domain.Common;

namespace TvShowsHub.Application.Common;

/// <summary>
/// Rate limiter configured for TV Maze API limits: 20 calls per 10 seconds
/// </summary>
public class TvMazeRateLimiter : RateLimiter
{
    private const int MaxCallsPer10Seconds = 20;
    private static readonly TimeSpan TimeWindow = TimeSpan.FromSeconds(10);

    public TvMazeRateLimiter() : base(MaxCallsPer10Seconds, TimeWindow)
    {
    }
}
