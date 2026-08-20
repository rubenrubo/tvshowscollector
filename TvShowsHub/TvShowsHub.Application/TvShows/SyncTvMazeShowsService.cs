using TvShowsHub.Domain.Common;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Application.TvShows;

public class SyncTvMazeShowsService(
    ITvShowClient client, 
    ITvShowRepository repository,
    IRateLimiter rateLimiter) : ISyncExternalTvShowsService
{
    public async Task SyncTvMazeShowsAsync()
    {
        await SyncTvMazeShowsAsync(startPage: 0);
    }

    private async Task SyncTvMazeShowsAsync(int startPage)
    {
        const int batchSize = 50; // Save every 50 pages
        var page = startPage;
        var batchFilteredShows = new List<TvShow>();
        var totalSynced = 0;
        var consecutiveFailures = 0;
        const int maxConsecutiveFailures = 3;

        while (true)
        {
            try
            {
                await rateLimiter.WaitIfNeededAsync();

                var shows = await client.GetTvShowsAsync(page);
                rateLimiter.RecordCall();

                consecutiveFailures = 0;

                if (shows.Length == 0)
                {
                    if (batchFilteredShows.Count > 0)
                    {
                        await repository.AddTvShowsAsync(batchFilteredShows);
                        totalSynced += batchFilteredShows.Count;
                    }
                    break;
                }

                var filteredShows = shows
                    .Where(s => s.Premiered is { Year: >= 2014, Month: >= 1, Day: >= 1 })
                    .ToList();

                batchFilteredShows.AddRange(filteredShows);
                page++;

                if (page % batchSize == 0 && batchFilteredShows.Count > 0)
                {
                    await repository.AddTvShowsAsync(batchFilteredShows);
                    totalSynced += batchFilteredShows.Count;
                    batchFilteredShows.Clear();
                    
                    Console.WriteLine($"Synced {totalSynced} shows (processed {page} pages)");
                }
            }
            catch (Exception ex)
            {
                consecutiveFailures++;
                Console.WriteLine($"Error fetching page {page}: {ex.Message}");

                // Save current batch before handling failure
                if (batchFilteredShows.Count > 0)
                {
                    try
                    {
                        await repository.AddTvShowsAsync(batchFilteredShows);
                        totalSynced += batchFilteredShows.Count;
                        batchFilteredShows.Clear();
                        Console.WriteLine($"Saved batch before failure. Total synced: {totalSynced}");
                    }
                    catch (Exception saveEx)
                    {
                        Console.WriteLine($"Failed to save batch: {saveEx.Message}");
                    }
                }

                if (consecutiveFailures >= maxConsecutiveFailures)
                {
                    Console.WriteLine($"Too many consecutive failures ({maxConsecutiveFailures}). Stopping sync at page {page}.");
                    throw new Exception($"Sync failed after {maxConsecutiveFailures} consecutive failures at page {page}", ex);
                }

                // Skip failed page and continue
                page++;
            }
        }

        Console.WriteLine($"Sync completed. Total shows synced: {totalSynced}");
    }
}