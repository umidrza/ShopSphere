using AuthService.Configuration;
using AuthService.Data;
using AuthService.Models;
using AuthService.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuthDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration
                .GetConnectionString("AuthDb")));

builder.Services
    .AddIdentity<ApplicationUser,
                 IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<
    IJwtService,
    JwtService>();

builder.Services.AddScoped<
    IRefreshTokenService,
    RefreshTokenService>();

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!))
            };
    })
    .AddGoogle(options =>
    {
        options.ClientId =
            builder.Configuration[
                "Google:ClientId"]!;

        options.ClientSecret =
            builder.Configuration[
                "Google:ClientSecret"]!;
    })
    .AddMicrosoftAccount(options =>
    {
        options.ClientId =
            builder.Configuration[
                "Microsoft:ClientId"]!;

        options.ClientSecret =
            builder.Configuration[
                "Microsoft:ClientSecret"]!;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
        AuthPolicies.ManageProducts,
        policy =>
        {
            policy.RequireClaim(
                ClaimConstants.CanManageProducts,
                "true");
        });

    options.AddPolicy(
        AuthPolicies.ManageOrders,
        policy =>
        {
            policy.RequireClaim(
                ClaimConstants.CanManageOrders,
                "true");
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope =
       app.Services.CreateScope())
{
    await RoleSeeder.SeedAsync(
        scope.ServiceProvider);

    await AdminSeeder.SeedAsync(
        scope.ServiceProvider);
}

app.Run();