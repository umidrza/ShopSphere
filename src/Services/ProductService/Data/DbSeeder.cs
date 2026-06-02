using ProductService.Models;

namespace ProductService.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(
        ProductDbContext context)
    {
        if (context.Categories.Any())
            return;

        context.Categories.AddRange(
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Electronics"
            },
            new Category
            {
                Id = Guid.NewGuid(),
                Name = "Books"
            });

        await context.SaveChangesAsync();
    }
}