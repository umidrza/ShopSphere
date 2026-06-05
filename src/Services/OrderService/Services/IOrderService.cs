using OrderService.DTOs;

namespace OrderService.Services;

public interface IOrderService
{
    Task<Guid> CreateOrderAsync(
        string userId,
        CreateOrderDto dto);
}