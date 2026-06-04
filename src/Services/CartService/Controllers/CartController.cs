using CartService.DTOs;
using CartService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CartService.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CartController
    : ControllerBase
{
    private readonly ICartService _service;

    public CartController(
        ICartService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier)!;

        return Ok(
            await _service
                .GetCartAsync(userId));
    }

    [HttpPost]
    public async Task<IActionResult>
        Add(AddToCartDto dto)
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier)!;

        await _service.AddItemAsync(
            userId,
            dto);

        return Ok();
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult>
        Remove(Guid productId)
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier)!;

        await _service.RemoveItemAsync(
            userId,
            productId);

        return NoContent();
    }
}