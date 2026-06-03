using AuthService.Data;
using AuthService.Models;

namespace AuthService.Services;

public class RefreshTokenService
    : IRefreshTokenService
{
    private readonly AuthDbContext _context;

    public RefreshTokenService(
        AuthDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken>
        GenerateAsync(ApplicationUser user)
    {
        var token = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = Guid.NewGuid().ToString(),
            ExpiresAt =
                DateTime.UtcNow.AddDays(7)
        };

        _context.RefreshTokens.Add(token);

        await _context.SaveChangesAsync();

        return token;
    }

    public async Task<RefreshToken?>
        GetAsync(string token)
    {
        return await _context.RefreshTokens
            .FindAsync(token);
    }
}