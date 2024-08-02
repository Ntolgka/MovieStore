namespace MovieStore.Schema.Customer;

public class CreateCustomerDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string IdentityNumber { get; set; }
    public List<int> FavoriteGenreIds { get; set; }
}