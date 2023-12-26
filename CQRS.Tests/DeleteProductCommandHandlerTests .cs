using CQRS.Application.Products.CommandHandlers;
using CQRS.Application.Products.Commands;

namespace CQRS.Tests;

[TestClass]
public class DeleteProductCommandHandlerTests : BaseTests
{
    private readonly CreateProductCommandHandler _createProductCommandHandler = new(WriteProductRepository);
    private readonly DeleteProductCommandHandler _deleteProductCommandHandler = new(WriteProductRepository);

    [TestMethod]
    public async Task DeleteProductShouldSucceed()
    {
        var createProductCommand = new CreateProductCommand
        {
            Name = "Test product" + Guid.NewGuid()
        };

        await _createProductCommandHandler.Handle(createProductCommand, CancellationToken.None);

        var createdProduct = ShopDbContext.Categories.FirstOrDefault(product => product.Name == createProductCommand.Name);
        Assert.IsNotNull(createdProduct);

        var deleteProductCommand = new DeleteProductCommand
        {
            Id = createdProduct!.Id,
        };

        await _deleteProductCommandHandler.Handle(deleteProductCommand, CancellationToken.None);

        var deletedProduct = ShopDbContext.Categories.FirstOrDefault(product => product.Name == createProductCommand.Name);

        Assert.IsNull(deletedProduct);
    }
}