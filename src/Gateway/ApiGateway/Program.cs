var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddReverseProxy()
    .LoadFromConfig(
        builder.Configuration
            .GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.MapGet("/health", () =>
{
    return Results.Ok(
        new
        {
            Service = "ApiGateway",
            Status = "Healthy"
        });
});

app.Run();