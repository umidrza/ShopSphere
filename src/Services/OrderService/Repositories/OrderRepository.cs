using OrderService.Data;
using OrderService.Models;

namespace OrderService.Repositories;

public class OrderRepository
    : IOrderRepository
{
    private readonly OrderDbContext _db;

    public OrderRepository(
        OrderDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(
        Order order)
    {
        await _db.Orders.AddAsync(order);
    }

    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
}