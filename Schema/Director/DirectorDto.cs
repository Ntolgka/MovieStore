using MovieStore.Schema.Movie;

namespace MovieStore.Schema.Director;

public class DirectorDto
{
    public int DirectorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public List<MovieDto> Movies { get; set; } = new List<MovieDto>();
}