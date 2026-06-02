using ProductService.Models;

namespace ProductService.Repositories;

public interface IProductRepository
{
    Task<List<Product>> GetAllAsync();

    Task<List<Product>> GetPagedAsync(int pageNumber, int pageSize);

    Task<List<Product>> SearchAsync(string searchTerm);

    Task<Product?> GetByIdAsync(Guid id);

    Task<Product> AddAsync(Product product);

    Task SaveChangesAsync();
}