using AuthService.DTOs;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController
    : ControllerBase
{
    private readonly UserManager<ApplicationUser>
        _userManager;

    private readonly IJwtService _jwtService;

    public AuthController(
        UserManager<ApplicationUser> userManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult>
        Register(RegisterDto dto)
    {
        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName
        };

        var result =
            await _userManager.CreateAsync(
                user,
                dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(
            user,
            "Customer");

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult>
        Login(LoginDto dto)
    {
        var user =
            await _userManager.FindByEmailAsync(
                dto.Email);

        if (user == null)
            return Unauthorized();

        var valid =
            await _userManager.CheckPasswordAsync(
                user,
                dto.Password);

        if (!valid)
            return Unauthorized();

        var roles =
            await _userManager.GetRolesAsync(
                user);

        var token =
            _jwtService.GenerateToken(
                user,
                roles);

        return Ok(
            new AuthResponseDto
            {
                AccessToken = token
            });
    }
}