using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OAuthController
    : ControllerBase
{
    [HttpGet("google")]
    public IActionResult GoogleLogin()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            "Google");
    }

    [HttpGet("microsoft")]
    public IActionResult MicrosoftLogin()
    {
        return Challenge(
            new AuthenticationProperties
            {
                RedirectUri = "/"
            },
            "Microsoft");
    }
}