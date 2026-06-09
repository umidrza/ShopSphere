using Microsoft.AspNetCore.Mvc;
using WebUI.Services;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _products;

    public HomeController(
        IProductService products)
    {
        _products = products;
    }

    public async Task<IActionResult> Index()
    {
        var products =
            await _products
                .GetProductsAsync();

        return View(products);
    }
}