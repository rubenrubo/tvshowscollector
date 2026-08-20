namespace TvShowsHub.Domain.Common;

public interface IRateLimiter
{
    Task WaitIfNeededAsync();
    void RecordCall();
}
