namespace OrderService.Models;

public class Order
{
    public Guid Id { get; set; }

    public string UserId { get; set; }
        = string.Empty;

    public DateTime CreatedAt { get; set; }

    public decimal TotalPrice { get; set; }

    public string Status { get; set; }
        = "Pending";

    public ICollection<OrderItem> Items
    { get; set; }
        = new List<OrderItem>();
}