using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Repositories;

public class CategoryRepository
    : ICategoryRepository
{
    private readonly ProductDbContext _context;

    public CategoryRepository(
        ProductDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>>
        GetAllAsync()
    {
        return await _context.Categories
            .ToListAsync();
    }

    public async Task<Category?>
        GetByIdAsync(Guid id)
    {
        return await _context.Categories
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(Category category)
    {
        await _context.Categories
            .AddAsync(category);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}