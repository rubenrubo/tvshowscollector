using Moq;
using TvShowsHub.Application.TvShows;
using TvShowsHub.Domain.Common;
using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.Tests.TvShows;

/// <summary>
/// Example tests showing how to test ManageTvShowsService with mocked dependencies
/// </summary>
[TestClass]
public class ManageTvShowsServiceTests
{
    [TestMethod]
    public async Task SyncTvMazeShowsAsync_FetchesAllPages_UntilEmpty()
    {
        // Arrange
        var mockClient = new Mock<ITvShowClient>();
        var mockRepository = new Mock<ITvShowRepository>();
        var mockRateLimiter = new Mock<IRateLimiter>();

        // Setup: First call returns shows, second call returns empty array
        mockClient.SetupSequence(c => c.GetTvShowsAsync(It.IsAny<int>()))
            .ReturnsAsync(new[]
            {
                new TvShow { Id = 1, Name = "Show 1", Premiered = DateOnly.FromDateTime(new DateTime(2015, 1, 1)) },
                new TvShow { Id = 2, Name = "Show 2", Premiered = DateOnly.FromDateTime(new DateTime(2013, 1, 1)) }
            })
            .ReturnsAsync(Array.Empty<TvShow>());

        var service = new ManageTvShowsService(
            mockClient.Object,
            mockRepository.Object,
            mockRateLimiter.Object);

        // Act
        await service.SyncTvMazeShowsAsync();

        // Assert
        mockClient.Verify(c => c.GetTvShowsAsync(0), Times.Once);
        mockClient.Verify(c => c.GetTvShowsAsync(1), Times.Once);
        mockRateLimiter.Verify(r => r.WaitIfNeededAsync(), Times.Exactly(2));
        mockRateLimiter.Verify(r => r.RecordCall(), Times.Exactly(2));
        
        // Verify only 1 show was added (2013 show filtered out)
        mockRepository.Verify(r => r.AddTvShowsAsync(
            It.Is<IEnumerable<TvShow>>(shows => 
                shows.Count() == 1 && shows.ElementAt(0).Id == 1)), 
            Times.Once);
    }

    [TestMethod]
    public async Task SyncTvMazeShowsAsync_FiltersShowsBefore2014()
    {
        // Arrange
        var mockClient = new Mock<ITvShowClient>();
        var mockRepository = new Mock<ITvShowRepository>();
        var mockRateLimiter = new Mock<IRateLimiter>();

        mockClient.Setup(c => c.GetTvShowsAsync(0))
            .ReturnsAsync(new[]
            {
                new TvShow { Id = 1, Name = "Old Show", Premiered = DateOnly.FromDateTime(new DateTime(2010, 5, 15)) },
                new TvShow { Id = 2, Name = "New Show", Premiered = DateOnly.FromDateTime(new DateTime(2020, 3, 10)) }
            });
        
        mockClient.Setup(c => c.GetTvShowsAsync(1))
            .ReturnsAsync(Array.Empty<TvShow>());

        var service = new ManageTvShowsService(
            mockClient.Object,
            mockRepository.Object,
            mockRateLimiter.Object);

        // Act
        await service.SyncTvMazeShowsAsync();

        // Assert
        mockRepository.Verify(r => r.AddTvShowsAsync(
            It.Is<IEnumerable<TvShow>>(shows => 
                shows.Count() == 1 && shows.ElementAt(0).Id == 2)), 
            Times.Once);
    }

    [TestMethod]
    public async Task SyncTvMazeShowsAsync_RespectsRateLimit()
    {
        // Arrange
        var mockClient = new Mock<ITvShowClient>();
        var mockRepository = new Mock<ITvShowRepository>();
        var mockRateLimiter = new Mock<IRateLimiter>();

        mockClient.SetupSequence(c => c.GetTvShowsAsync(It.IsAny<int>()))
            .ReturnsAsync(new[] { new TvShow { Id = 1, Name = "Show", Premiered = DateOnly.FromDateTime(new DateTime(2015, 1, 1)) } })
            .ReturnsAsync(new[] { new TvShow { Id = 2, Name = "Show2", Premiered = DateOnly.FromDateTime(new DateTime(2016, 1, 1)) } })
            .ReturnsAsync(Array.Empty<TvShow>());

        var service = new ManageTvShowsService(
            mockClient.Object,
            mockRepository.Object,
            mockRateLimiter.Object);

        // Act
        await service.SyncTvMazeShowsAsync();

        // Assert - Rate limiter was called before each API call
        mockRateLimiter.Verify(r => r.WaitIfNeededAsync(), Times.Exactly(3));
        mockRateLimiter.Verify(r => r.RecordCall(), Times.Exactly(3));
    }
}
