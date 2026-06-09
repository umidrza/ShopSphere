using WebUI.ViewModels;

namespace WebUI.Services;

public interface IProductService
{
    Task<List<ProductVm>>
        GetProductsAsync();
}