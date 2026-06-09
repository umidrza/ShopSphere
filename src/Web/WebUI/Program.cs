using WebUI.Services;

var builder =
    WebApplication
        .CreateBuilder(args);

builder.Services
    .AddControllersWithViews();

builder.Services
.AddHttpClient<
IProductService,
ProductService>(
client =>
{
    client.BaseAddress =
    new Uri(
    builder.Configuration[
    "ApiGateway:BaseUrl"]!);
});

var app =
builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
name: "default",
pattern:
"{controller=Home}/{action=Index}/{id?}");

app.Run();