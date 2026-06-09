namespace NotificationService.Events;

public class OrderCreatedEvent
{
    public Guid OrderId { get; set; }

    public string UserId { get; set; }
        = string.Empty;

    public decimal TotalPrice { get; set; }
}