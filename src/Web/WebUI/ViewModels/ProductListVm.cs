namespace WebUI.ViewModels;


public class ProductListVm
{
    public List<ProductVm> Products { get; set; } = [];

    public List<CategoryVm> Categories { get; set; } = [];

    public Guid? SelectedCategoryId { get; set; }

    public string? SearchTerm { get; set; }
}