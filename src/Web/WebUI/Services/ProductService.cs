using WebUI.ViewModels;

namespace WebUI.Services;

public class ProductService
    : IProductService
{
    private readonly HttpClient
        _client;

    public ProductService(
        HttpClient client)
    {
        _client = client;
    }

    public async Task<
        List<ProductVm>>
        GetProductsAsync()
    {
        return await _client
            .GetFromJsonAsync<
                List<ProductVm>>(
                    "/api/products")
            ?? [];
    }
}