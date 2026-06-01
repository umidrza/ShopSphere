using ProductService.Models;

namespace ProductService.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(Guid id);

    Task<Product> AddAsync(Product product);

    Task SaveChangesAsync();
}