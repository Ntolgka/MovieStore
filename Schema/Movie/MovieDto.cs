using MovieStore.Schema.Actor;
using MovieStore.Schema.Director;

namespace MovieStore.Schema.Movie;

public class MovieDto
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
    public List<ActorDto> Actors { get; set; }
    public List<DirectorDto> Directors { get; set; }
}