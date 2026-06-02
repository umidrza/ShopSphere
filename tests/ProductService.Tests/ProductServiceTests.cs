using FluentAssertions;
using Moq;
using ProductService.Repositories;
using ProductService.Services;

public class ProductServiceTests
{
    [Fact]
    public async Task Should_Return_Products()
    {
        var repo =
            new Mock<IProductRepository>();

        repo.Setup(x =>
            x.GetAllAsync())
            .ReturnsAsync(new List<ProductService.Models.Product>());

        var service =
            new ProductService.Services.ProductService(
                repo.Object);

        var result =
            await service.GetAllAsync();

        result.Should().NotBeNull();
    }
}