namespace CartService.Clients;

public interface IProductClient
{
    Task<ProductDto?> GetProductAsync(
        Guid productId);
}