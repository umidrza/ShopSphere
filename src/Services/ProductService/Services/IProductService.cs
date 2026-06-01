using ProductService.DTOs;

namespace ProductService.Services;

public interface IProductService
{
    Task<List<ProductDto>> GetAllAsync();

    Task<ProductDto?> GetByIdAsync(Guid id);

    Task<ProductDto> CreateAsync(
        CreateProductDto dto);
}