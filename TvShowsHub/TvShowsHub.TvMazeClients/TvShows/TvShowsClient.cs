using TvShowsHub.Domain.TvShows;

namespace TvShowsHub.TvMazeClient.TvShows;

public class TvShowsClient : BaseClient, ITvShowClient
{
    private readonly HttpClient _client;
    
    public TvShowsClient(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("TvMaze");
    }
    
    public async Task<TvShow[]> GetTvShows()
    {
        var apiResults = await GetAsync<TvShowsDto[]>(_client, "/shows?page=1");

        return apiResults.Select(apiResult => new TvShow
        {
            Id = apiResult.Id ?? 0,
            Name = apiResult.Name,
            Language = apiResult.Language,
            Premiered = apiResult.Premiered,
            Genres = apiResult.Genres,
            Summary = apiResult.Summary
        }).ToArray();
    }
}