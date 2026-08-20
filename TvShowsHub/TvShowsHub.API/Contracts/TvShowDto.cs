namespace TvShowsHub.API.Contracts;

public record TvShowDto
{
    public int? Id { get; init; }
    public string? Name { get; init; }
    public string? Language { get; init; }
    public DateOnly? Premiered { get; init; }
    public string[]? Genres { get; init; }
    public string? Summary { get; init; }
}