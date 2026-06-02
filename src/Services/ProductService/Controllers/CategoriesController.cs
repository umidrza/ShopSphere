using Microsoft.AspNetCore.Mvc;
using ProductService.Services;

namespace ProductService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController
    : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(
        ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(
            await _service.GetAllAsync());
    }
}