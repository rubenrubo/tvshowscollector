namespace TvShowsHub.TvMazeClient.TvShows;

public record TvShowsDto
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public string? Language { get; init; }
    public DateOnly? Premiered { get; init; }
    public DateOnly? Ended { get; init; }
    public string[]? Genres { get; init; }
    public string? Summary { get; init; }
    public string? Url { get; set; }
}