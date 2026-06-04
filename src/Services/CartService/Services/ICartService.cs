using CartService.DTOs;
using CartService.Models;

namespace CartService.Services;

public interface ICartService
{
    Task<Cart?> GetCartAsync(
        string userId);

    Task AddItemAsync(
        string userId,
        AddToCartDto dto);

    Task RemoveItemAsync(
        string userId,
        Guid productId);

    Task ClearCartAsync(
        string userId);
}