namespace MovieStore.Schema.Customer;

public class CustomerDto
{
    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public List<int> FavoriteGenreIds { get; set; }
}