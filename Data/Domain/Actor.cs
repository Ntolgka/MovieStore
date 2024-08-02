using System.ComponentModel.DataAnnotations.Schema;
using MovieStore.Base;

namespace MovieStore.Data.Domain;

public class Actor : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ActorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Movie> Movies { get; set; }
}