using CQRS.Application.Products.CommandHandlers;
using CQRS.Application.Products.Commands;

namespace CQRS.Tests;

[TestClass]
public class UpdateProductCommandHandlerTests : BaseTests
{
    private readonly CreateProductCommandHandler _createProductCommandHandler = new(WriteProductRepository);
    private readonly UpdateProductCommandHandler _updateProductCommandHandler = new(WriteProductRepository);

    [TestMethod]
    public async Task UpdateProductShouldSucceed()
    {
        var createProductCommand = new CreateProductCommand
        {
            Name = "Test product" + Guid.NewGuid()
        };

        await _createProductCommandHandler.Handle(createProductCommand, CancellationToken.None);

        var createdProduct = ShopDbContext.Categories.FirstOrDefault(product => product.Name == createProductCommand.Name);

        var updateProductCommand = new UpdateProductCommand
        {
            Id = createdProduct!.Id,
            Name = "Test product updated" + Guid.NewGuid()
        };

        await _updateProductCommandHandler.Handle(updateProductCommand, CancellationToken.None);

        var updatedProduct = ShopDbContext.Categories.FirstOrDefault(product => product.Name == updateProductCommand.Name);

        Assert.IsNotNull(updatedProduct);
        Assert.AreEqual(updatedProduct.Name, updateProductCommand.Name);
    }
}