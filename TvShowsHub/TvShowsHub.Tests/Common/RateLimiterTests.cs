using TvShowsHub.Application.Common;

namespace TvShowsHub.Tests.Common;

[TestClass]
public class RateLimiterTests
{
    [TestMethod]
    public async Task WaitIfNeededAsync_DoesNotWait_WhenUnderLimit()
    {
        // Arrange
        var rateLimiter = new RateLimiter(maxCalls: 5, timeWindow: TimeSpan.FromSeconds(1));
        var stopwatch = System.Diagnostics.Stopwatch.StartNew();

        // Act - Make 5 calls (under limit)
        for (int i = 0; i < 5; i++)
        {
            await rateLimiter.WaitIfNeededAsync();
            rateLimiter.RecordCall();
        }

        stopwatch.Stop();

        // Assert - Should complete quickly (no waiting)
        Assert.IsTrue(stopwatch.ElapsedMilliseconds < 100);
    }

    [TestMethod]
    public async Task WaitIfNeededAsync_Waits_WhenAtLimit()
    {
        // Arrange
        var rateLimiter = new RateLimiter(maxCalls: 3, timeWindow: TimeSpan.FromSeconds(1));

        // Act - Make 3 calls to hit the limit
        for (int i = 0; i < 3; i++)
        {
            await rateLimiter.WaitIfNeededAsync();
            rateLimiter.RecordCall();
        }

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        // This 4th call should wait
        await rateLimiter.WaitIfNeededAsync();
        
        stopwatch.Stop();

        // Assert - Should have waited approximately 1 second
        Assert.IsTrue(stopwatch.ElapsedMilliseconds >= 900); // Allow some tolerance
    }

    [TestMethod]
    public async Task RateLimiter_AllowsCallsAfterTimeWindow()
    {
        // Arrange
        var rateLimiter = new RateLimiter(maxCalls: 2, timeWindow: TimeSpan.FromMilliseconds(500));

        // Act - Make 2 calls
        await rateLimiter.WaitIfNeededAsync();
        rateLimiter.RecordCall();
        await rateLimiter.WaitIfNeededAsync();
        rateLimiter.RecordCall();

        // Wait for time window to pass
        await Task.Delay(600);

        var stopwatch = System.Diagnostics.Stopwatch.StartNew();
        
        // Should not wait since time window passed
        await rateLimiter.WaitIfNeededAsync();
        rateLimiter.RecordCall();
        
        stopwatch.Stop();

        // Assert - Should complete quickly
        Assert.IsTrue(stopwatch.ElapsedMilliseconds < 100);
    }

    [TestMethod]
    public async Task RateLimiter_ThreadSafe()
    {
        // Arrange
        var rateLimiter = new RateLimiter(maxCalls: 10, timeWindow: TimeSpan.FromSeconds(1));
        var callCount = 0;

        // Act - Make concurrent calls
        var tasks = Enumerable.Range(0, 15).Select(async _ =>
        {
            await rateLimiter.WaitIfNeededAsync();
            Interlocked.Increment(ref callCount);
            rateLimiter.RecordCall();
        });

        await Task.WhenAll(tasks);

        // Assert - All calls completed
        Assert.AreEqual(15, callCount);
    }
}
