namespace CartService.Clients;

public class ProductClient
    : IProductClient
{
    private readonly HttpClient _client;

    public ProductClient(
        HttpClient client)
    {
        _client = client;
    }

    public async Task<ProductDto?>
        GetProductAsync(Guid productId)
    {
        return await _client
            .GetFromJsonAsync<ProductDto>(
                $"api/products/{productId}");
    }
}