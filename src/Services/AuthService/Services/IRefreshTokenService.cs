using AuthService.Models;

namespace AuthService.Services;

public interface IRefreshTokenService
{
    Task<RefreshToken> GenerateAsync(ApplicationUser user);

    Task<RefreshToken?> GetAsync(string token);
}