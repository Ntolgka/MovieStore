namespace MovieStore.Schema.Order;

public class OrderDto
{
    public int OrderId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public int MovieId { get; set; }
    public string MovieTitle { get; set; }
    public decimal Price { get; set; }
    public DateTime PurchaseDate { get; set; }
}