using ProductService.DTOs;
using ProductService.Repositories;

namespace ProductService.Services;

public class CategoryService
    : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(
        ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<CategoryDto>>
        GetAllAsync()
    {
        var categories =
            await _repo.GetAllAsync();

        return categories
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToList();
    }
}