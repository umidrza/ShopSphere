using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController
    : ControllerBase
{
    [Authorize(Roles = "Admin")]
    [HttpGet("dashboard")]
    public IActionResult Dashboard()
    {
        return Ok(
            "Admin Dashboard");
    }
}