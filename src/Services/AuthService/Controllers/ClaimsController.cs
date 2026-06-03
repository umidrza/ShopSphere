using AuthService.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClaimsController
    : ControllerBase
{
    [Authorize(
        Policy =
            AuthPolicies.ManageProducts)]
    [HttpGet("products")]
    public IActionResult Products()
    {
        return Ok(
            "Products Permission");
    }
}