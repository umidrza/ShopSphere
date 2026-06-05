using OrderService.DTOs;
using OrderService.Events;
using OrderService.Messaging;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Services;

public class OrderService
    : IOrderService
{
    private readonly IOrderRepository _repo;

    private readonly IMessagePublisher
        _publisher;

    public OrderService(
        IOrderRepository repo,
        IMessagePublisher publisher)
    {
        _repo = repo;
        _publisher = publisher;
    }

    public async Task<Guid>
        CreateOrderAsync(
            string userId,
            CreateOrderDto dto)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CreatedAt = DateTime.UtcNow,
            TotalPrice =
                dto.Items.Sum(x =>
                    x.Price * x.Quantity),

            Items =
                dto.Items.Select(x =>
                    new OrderItem
                    {
                        Id = Guid.NewGuid(),
                        ProductId = x.ProductId,
                        ProductName =
                            x.ProductName,
                        Price = x.Price,
                        Quantity = x.Quantity
                    }).ToList()
        };

        await _repo.AddAsync(order);

        await _repo.SaveChangesAsync();

        await _publisher.PublishAsync(
            new OrderCreatedEvent
            {
                OrderId = order.Id,
                UserId = userId,
                TotalPrice =
                    order.TotalPrice
            });

        return order.Id;
    }
}