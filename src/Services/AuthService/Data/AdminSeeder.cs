using AuthService.Models;
using Microsoft.AspNetCore.Identity;

namespace AuthService.Data;

public static class AdminSeeder
{
    public static async Task SeedAsync(
        IServiceProvider services)
    {
        var userManager =
            services.GetRequiredService<
                UserManager<ApplicationUser>>();

        var admin =
            await userManager
                .FindByEmailAsync(
                    "admin@shopsphere.com");

        if (admin != null)
            return;

        admin = new ApplicationUser
        {
            UserName =
                "admin@shopsphere.com",
            Email =
                "admin@shopsphere.com"
        };

        await userManager.CreateAsync(
            admin,
            "Admin123!");

        await userManager.AddToRoleAsync(
            admin,
            "Admin");
    }
}