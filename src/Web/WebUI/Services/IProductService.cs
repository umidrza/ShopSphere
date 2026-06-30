using WebUI.ViewModels;

namespace WebUI.Services;

public interface IProductService
{
    Task<List<ProductVm>>
        GetProductsAsync();

    Task<List<ProductVm>>
        GetProductsByCategoryAsync(Guid categoryId);

    Task<ProductVm?>
        GetProductByIdAsync(Guid id);

    Task<List<CategoryVm>>
        GetCategoriesAsync();

    Task<ProductListVm>
        GetProductsWithCategoriesAsync(
            Guid? categoryId = null,
            string? searchTerm = null);
}