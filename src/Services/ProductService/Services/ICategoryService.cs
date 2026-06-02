using ProductService.DTOs;

namespace ProductService.Services;

public interface ICategoryService
{
    Task<List<CategoryDto>> GetAllAsync();
}