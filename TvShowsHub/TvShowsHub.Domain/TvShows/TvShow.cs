namespace TvShowsHub.Domain.TvShows;

public class TvShow
{
    public TvShow() {}
    
    public TvShow(AddTvShowSpec spec)
    {
        Name = spec.Name;
        Language = spec.Language;
        Premiered = spec.Premiered;
        Genres = spec.Genres;
        Summary = spec.Summary;
    }
    
    public void Update(UpdateTvShowSpec spec)
    {
        Name = spec.Name;
        Language = spec.Language;
        Premiered = spec.Premiered;
        Genres = spec.Genres;
        Summary = spec.Summary;
    }
    public int? Id { get; init; }
    public int? TvMazeId { get; set; }
    public string? Name { get; set; }
    public string? Language { get; set; }
    public DateOnly? Premiered { get; set; }
    public string[]? Genres { get; set; }
    public string? Summary { get; set; }
}