using CQRS.Application.Categories.CommandHandlers;
using CQRS.Application.Categories.Commands;
using CQRS.Application.Products.CommandHandlers;
using CQRS.Application.Products.Commands;
using CQRS.Application.Products.Queries;
using CQRS.Application.Products.QueryHandlers;

using Microsoft.EntityFrameworkCore;

namespace CQRS.Tests;

[TestClass]
public class CreateProductCommandHandlerTests : BaseTests
{
    private readonly CreateCategoryCommandHandler _createCategoryCommandHandler = new(WriteCategoryRepository);
    private readonly CreateProductCommandHandler _createProductCommandHandler = new(WriteProductRepository);
    private readonly SyncProductsCommandHandler _syncProductsCommandHandler = new(EventLogRepository, SyncProductsRepository);
    private readonly GetProductByIdQueryHandler _getProductByIdQueryHandler = new(ReadProductRepository);


    [TestMethod]
    public async Task CreateProductShouldAddProductToSqlDatabase()
    {
        var createCategoryCommand = new CreateCategoryCommand
        {
            Name = "Test category" + Guid.NewGuid()
        };

        await _createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);

        var createdCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);

        var createProductCommand = new CreateProductCommand
        {
            CategoryId = createdCategory!.Id,
            Name = "Test product" + Guid.NewGuid(),
            Images = ["Image" + Guid.NewGuid()]
        };

        await _createProductCommandHandler.Handle(createProductCommand, CancellationToken.None);

        var createdProduct = ShopDbContext.Products.Include(product => product.Images).FirstOrDefault(product => product.Name == createProductCommand.Name);

        Assert.IsNotNull(createdProduct);
        Assert.AreEqual(createdProduct.Name, createProductCommand.Name);
        Assert.IsTrue(createdProduct.Images.Count > 0);
    }

    [TestMethod]
    public async Task CreateProductShouldAddProductToMongoDatabaseAfterSync()
    {
        var createCategoryCommand = new CreateCategoryCommand
        {
            Name = "Test category" + Guid.NewGuid()
        };

        await _createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);

        var createdCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);

        var createProductCommand = new CreateProductCommand
        {
            CategoryId = createdCategory!.Id,
            Name = "Test product" + Guid.NewGuid(),
            Images = ["Image" + Guid.NewGuid()]
        };

        await _createProductCommandHandler.Handle(createProductCommand, CancellationToken.None);

        await _syncProductsCommandHandler.Handle(new SyncProductsCommand(), CancellationToken.None);

        var createdProduct = ShopDbContext.Products.Include(product => product.Images).FirstOrDefault(product => product.Name == createProductCommand.Name);

        var createdMongoProduct = await _getProductByIdQueryHandler.Handle(new GetProductByIdQuery { Id = createdProduct!.Id }, CancellationToken.None);

        Assert.IsNotNull(createdMongoProduct);
        Assert.AreEqual(createdMongoProduct.Name, createdProduct.Name);
        Assert.IsTrue(createdMongoProduct.Images.Count > 0);
    }
}