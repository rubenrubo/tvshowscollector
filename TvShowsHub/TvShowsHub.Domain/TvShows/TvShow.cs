namespace TvShowsHub.Domain.TvShows;

public class TvShow
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public string? Language { get; init; }
    public DateOnly? Premiered { get; init; }
    public string[]? Genres { get; init; }
    public string? Summary { get; init; }
}