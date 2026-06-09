using NotificationService.Events;

namespace NotificationService.Services;

public interface IEmailService
{
    Task SendOrderCreatedAsync(
        OrderCreatedEvent order);
}