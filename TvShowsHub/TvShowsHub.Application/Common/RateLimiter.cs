using TvShowsHub.Domain.Common;

namespace TvShowsHub.Application.Common;

public class RateLimiter : IRateLimiter
{
    private readonly int _maxCalls;
    private readonly TimeSpan _timeWindow;
    private readonly Queue<DateTime> _callTimestamps = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public RateLimiter(int maxCalls, TimeSpan timeWindow)
    {
        _maxCalls = maxCalls;
        _timeWindow = timeWindow;
    }

    public async Task WaitIfNeededAsync()
    {
        await _semaphore.WaitAsync();
        try
        {
            var now = DateTime.UtcNow;

            // Remove timestamps outside the time window
            while (_callTimestamps.Count > 0 && now - _callTimestamps.Peek() >= _timeWindow)
            {
                _callTimestamps.Dequeue();
            }

            // If we're at the limit, calculate wait time
            if (_callTimestamps.Count >= _maxCalls)
            {
                var oldestCall = _callTimestamps.Peek();
                var waitTime = _timeWindow - (now - oldestCall);
                
                if (waitTime > TimeSpan.Zero)
                {
                    await Task.Delay(waitTime);
                    
                    // Clean up again after waiting
                    now = DateTime.UtcNow;
                    while (_callTimestamps.Count > 0 && now - _callTimestamps.Peek() >= _timeWindow)
                    {
                        _callTimestamps.Dequeue();
                    }
                }
            }
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public void RecordCall()
    {
        _callTimestamps.Enqueue(DateTime.UtcNow);
    }
}
