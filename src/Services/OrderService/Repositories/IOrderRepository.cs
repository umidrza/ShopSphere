using OrderService.Models;

namespace OrderService.Repositories;

public interface IOrderRepository
{
    Task AddAsync(Order order);

    Task SaveChangesAsync();
}