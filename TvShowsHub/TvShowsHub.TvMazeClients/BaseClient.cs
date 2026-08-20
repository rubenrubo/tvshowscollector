using System.Text.Json;

namespace TvShowsHub.TvMazeClient;

public abstract class BaseClient
{
    private readonly JsonSerializerOptions _serializeOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
    
    protected async Task<TResponse> GetAsync<TResponse>(HttpClient client, string endpoint) where TResponse : class
    {
        using var httpResponseMessage = await client.GetAsync(endpoint);
        
        return await ProcessResponseAsync<TResponse>(httpResponseMessage, endpoint);
    }
    
    private async Task<TResponse> ProcessResponseAsync<TResponse>(HttpResponseMessage httpResponseMessage, string endpoint) where TResponse : class
    {
        var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();

        if (!httpResponseMessage.IsSuccessStatusCode)
        {
            throw new Exception("API request failed");
        }

        try
        {
            var result = JsonSerializer.Deserialize<TResponse>(responseContent, _serializeOptions);
            return result ?? throw new Exception("Deserialized result is null.");
        }
        catch (JsonException)
        { }
        
        throw new Exception("Something unexpected happened while processing the client response.");
    }
}