using ProductService.DTOs;
using ProductService.Models;
using ProductService.Repositories;

namespace ProductService.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(
        IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ProductDto>> GetAllAsync()
    {
        var products =
            await _repository.GetAllAsync();

        return products.Select(MapToDto).ToList();
    }

    public async Task<ProductDto?> GetByIdAsync(Guid id)
    {
        var product =
            await _repository.GetByIdAsync(id);

        if (product == null)
            return null;

        return MapToDto(product);
    }

    public async Task<ProductDto> CreateAsync(
        CreateProductDto dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Stock = dto.Stock,
            ImageUrl = dto.ImageUrl,
            CategoryId = dto.CategoryId
        };

        await _repository.AddAsync(product);

        await _repository.SaveChangesAsync();

        return MapToDto(product);
    }

    private static ProductDto MapToDto(Product p)
    {
        return new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Stock = p.Stock,
            ImageUrl = p.ImageUrl,
            CategoryId = p.CategoryId
        };
    }
}