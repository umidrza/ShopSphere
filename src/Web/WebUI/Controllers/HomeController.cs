using Microsoft.AspNetCore.Mvc;
using WebUI.Services;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly IProductService _products;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IProductService products,
        ILogger<HomeController> logger)
    {
        _products = products;
        _logger = logger;
    }

    public async Task<IActionResult> Index(
        Guid? categoryId = null,
        string? search = null)
    {
        _logger.LogInformation(
            "Home Index called - CategoryId: {CategoryId}, Search: {Search}",
            categoryId,
            search ?? "none");

        var productList = await _products
            .GetProductsWithCategoriesAsync(categoryId, search);

        return View(productList);
    }

    public async Task<IActionResult> ProductDetail(Guid id)
    {
        _logger.LogInformation(
            "Product detail requested - ProductId: {ProductId}",
            id);

        var product = await _products.GetProductByIdAsync(id);

        if (product == null)
        {
            _logger.LogWarning(
                "Product not found - ProductId: {ProductId}",
                id);

            return NotFound();
        }

        return View(product);
    }
}