using MovieStore.Base;

namespace MovieStore.Data.Domain;

public class Director : BaseEntity
{
    public int DirectorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Movie> Movies { get; set; }
}