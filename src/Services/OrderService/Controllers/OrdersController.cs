using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderService.DTOs;
using OrderService.Services;
using System.Security.Claims;

namespace OrderService.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class OrdersController
    : ControllerBase
{
    private readonly IOrderService
        _service;

    public OrdersController(
        IOrderService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult>
        Create(CreateOrderDto dto)
    {
        var userId =
            User.FindFirstValue(
                ClaimTypes.NameIdentifier)!;

        var orderId =
            await _service
                .CreateOrderAsync(
                    userId,
                    dto);

        return Ok(new
        {
            OrderId = orderId
        });
    }
}