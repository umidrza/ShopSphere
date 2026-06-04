using CartService.Clients;
using CartService.DTOs;
using CartService.Models;
using StackExchange.Redis;
using System.Text.Json;

namespace CartService.Services;

public class CartService : ICartService
{
    private readonly IDatabase _redis;

    private readonly IProductClient
        _productClient;

    public CartService(
        IConnectionMultiplexer redis,
        IProductClient productClient)
    {
        _redis = redis.GetDatabase();
        _productClient = productClient;
    }

    public async Task<Cart?>
        GetCartAsync(string userId)
    {
        var data =
            await _redis.StringGetAsync(
                $"cart:{userId}");

        if (!data.HasValue)
            return new Cart
            {
                UserId = userId
            };

        return JsonSerializer.Deserialize<Cart>((string)data!);
    }

    public async Task AddItemAsync(
        string userId,
        AddToCartDto dto)
    {
        var cart =
            await GetCartAsync(userId);

        var product =
            await _productClient
                .GetProductAsync(
                    dto.ProductId);

        if (product == null)
            throw new Exception(
                "Product not found");

        cart!.Items.Add(
            new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = dto.Quantity
            });

        await SaveCartAsync(cart);
    }

    public async Task RemoveItemAsync(
        string userId,
        Guid productId)
    {
        var cart =
            await GetCartAsync(userId);

        cart!.Items.RemoveAll(
            x => x.ProductId == productId);

        await SaveCartAsync(cart);
    }

    public async Task ClearCartAsync(
        string userId)
    {
        await _redis.KeyDeleteAsync(
            $"cart:{userId}");
    }

    private async Task SaveCartAsync(
        Cart cart)
    {
        await _redis.StringSetAsync(
            $"cart:{cart.UserId}",
            JsonSerializer.Serialize(cart));
    }
}