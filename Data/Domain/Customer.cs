using System.ComponentModel.DataAnnotations.Schema;
using MovieStore.Base;

namespace MovieStore.Data.Domain;

public class Customer : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CustomerId { get; set; }
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Genre> FavoriteGenres { get; set; } = new List<Genre>();
}