namespace MovieStore.Schema.Movie;

public class UpdateMovieDto
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
    public List<int> ActorIds { get; set; }
    public List<int> DirectorIds { get; set; }
}