using MovieStore.Base;

namespace MovieStore.Data.Domain;

public class Movie : BaseEntity
{
    public int MovieId { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }

    public ICollection<Actor> Actors { get; set; }
    public ICollection<Director> Directors { get; set; }
    public ICollection<Order> Orders { get; set; }
}