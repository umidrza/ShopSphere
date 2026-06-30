using WebUI.ViewModels;

namespace WebUI.Services;

public class ProductService
    : IProductService
{
    private readonly HttpClient
        _client;
    private readonly ILogger<
        ProductService>
        _logger;

    public ProductService(
        HttpClient client,
        ILogger<ProductService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<
        List<ProductVm>>
        GetProductsAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all products");

            var products = await _client
                .GetFromJsonAsync<
                    List<ProductVm>>(
                        "/api/products") ?? [];

            _logger.LogInformation(
                "Retrieved {ProductCount} products",
                products.Count);

            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching products");

            return [];
        }
    }

    public async Task<
        List<ProductVm>>
        GetProductsByCategoryAsync(
            Guid categoryId)
    {
        try
        {
            _logger.LogInformation(
                "Fetching products for category {CategoryId}",
                categoryId);

            var products = await _client
                .GetFromJsonAsync<
                    List<ProductVm>>(
                        $"/api/products?categoryId={categoryId}") ?? [];

            _logger.LogInformation(
                "Retrieved {ProductCount} products for category {CategoryId}",
                products.Count,
                categoryId);

            return products;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching products for category {CategoryId}",
                categoryId);

            return [];
        }
    }

    public async Task<
        ProductVm?>
        GetProductByIdAsync(
            Guid id)
    {
        try
        {
            _logger.LogInformation(
                "Fetching product with ID {ProductId}",
                id);

            var product = await _client
                .GetFromJsonAsync<
                    ProductVm>(
                        $"/api/products/{id}");

            _logger.LogInformation(
                "Retrieved product {ProductId}",
                id);

            return product;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching product {ProductId}",
                id);

            return null;
        }
    }

    public async Task<
        List<CategoryVm>>
        GetCategoriesAsync()
    {
        try
        {
            _logger.LogInformation("Fetching all categories");

            var categories = await _client
                .GetFromJsonAsync<
                    List<CategoryVm>>(
                        "/api/categories") ?? [];

            _logger.LogInformation(
                "Retrieved {CategoryCount} categories",
                categories.Count);

            return categories;
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching categories");

            return [];
        }
    }

    public async Task<
        ProductListVm>
        GetProductsWithCategoriesAsync(
            Guid? categoryId = null,
            string? searchTerm = null)
    {
        try
        {
            _logger.LogInformation(
                "Fetching products and categories - CategoryId: {CategoryId}, SearchTerm: {SearchTerm}",
                categoryId,
                searchTerm ?? "none");

            var productsTask = GetProductsAsync();
            var categoriesTask = GetCategoriesAsync();

            await Task.WhenAll(productsTask, categoriesTask);

            var products = productsTask.Result;
            var categories = categoriesTask.Result;

            // Filter by category if specified
            if (categoryId.HasValue)
            {
                products = products
                    .Where(p => p.CategoryId == categoryId.Value)
                    .ToList();
            }

            // Filter by search term if specified
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                var term = searchTerm.ToLower();
                products = products
                    .Where(p =>
                        p.Name.ToLower().Contains(term) ||
                        p.Description.ToLower().Contains(term))
                    .ToList();
            }

            return new ProductListVm
            {
                Products = products,
                Categories = categories,
                SelectedCategoryId = categoryId,
                SearchTerm = searchTerm
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error fetching products with categories");

            return new ProductListVm();
        }
    }
}