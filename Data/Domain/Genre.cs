using System.ComponentModel.DataAnnotations.Schema;
using MovieStore.Base;

namespace MovieStore.Data.Domain;

public class Genre : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Customer> Customers { get; set; } = new List<Customer>();
}