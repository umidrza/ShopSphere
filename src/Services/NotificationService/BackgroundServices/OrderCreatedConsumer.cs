using NotificationService.Events;
using NotificationService.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace NotificationService.BackgroundServices;

public class OrderCreatedConsumer : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IConfiguration _config;

    public OrderCreatedConsumer(
        IServiceScopeFactory scopeFactory,
        IConfiguration config)
    {
        _scopeFactory = scopeFactory;
        _config = config;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            HostName = _config["RabbitMQ:Host"]
        };

        var connection =
            await factory.CreateConnectionAsync(
                cancellationToken: stoppingToken);

        var channel =
            await connection.CreateChannelAsync(
                cancellationToken: stoppingToken);

        await channel.QueueDeclareAsync(
            queue: "order-created",
            durable: false,
            exclusive: false,
            autoDelete: false,
            cancellationToken: stoppingToken);

        var consumer =
            new AsyncEventingBasicConsumer(
                channel);

        consumer.ReceivedAsync +=
            async (_, eventArgs) =>
            {
                using var scope =
                    _scopeFactory.CreateScope();

                var email =
                    scope.ServiceProvider
                        .GetRequiredService<IEmailService>();

                var body =
                    eventArgs.Body.ToArray();

                var json =
                    Encoding.UTF8.GetString(body);

                var evt =
                    JsonSerializer.Deserialize<OrderCreatedEvent>(
                        json);

                if (evt != null)
                {
                    await email.SendOrderCreatedAsync(evt);
                }
            };

        await channel.BasicConsumeAsync(
            queue: "order-created",
            autoAck: true,
            consumer: consumer,
            cancellationToken: stoppingToken);

        await Task.Delay(
            Timeout.Infinite,
            stoppingToken);
    }
}