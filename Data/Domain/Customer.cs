using MovieStore.Base;

namespace MovieStore.Data.Domain;

public class Customer : BaseEntity
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Order> Orders { get; set; }
}