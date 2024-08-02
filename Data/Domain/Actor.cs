using MovieStore.Base;

namespace MovieStore.Data.Domain;

public class Actor : BaseEntity
{
    public int ActorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Movie> Movies { get; set; }
}