using NotificationService.Events;

namespace NotificationService.Services;

public class EmailService
    : IEmailService
{
    private readonly ILogger<
        EmailService> _logger;

    public EmailService(
        ILogger<EmailService> logger)
    {
        _logger = logger;
    }

    public Task SendOrderCreatedAsync(
        OrderCreatedEvent order)
    {
        _logger.LogInformation(
            """
            EMAIL SENT

            OrderId:{OrderId}

            Total:{Total}
            """,
            order.OrderId,
            order.TotalPrice);

        return Task.CompletedTask;
    }
}