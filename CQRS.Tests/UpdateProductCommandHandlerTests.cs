using CQRS.Application.Categories.CommandHandlers;
using CQRS.Application.Categories.Commands;
using CQRS.Application.Products.CommandHandlers;
using CQRS.Application.Products.Commands;
using CQRS.Application.Products.Queries;
using CQRS.Application.Products.QueryHandlers;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Tests
{
    [TestClass]
    public class UpdateProductCommandHandlerTests : BaseTests
    {
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler = new(WriteCategoryRepository);
        private readonly CreateProductCommandHandler _createProductCommandHandler = new(WriteProductRepository);
        private readonly UpdateProductCommandHandler _updateProductCommandHandler = new(WriteProductRepository);
        private readonly GetProductByIdQueryHandler _getProductByIdQueryHandler = new(ReadProductRepository);
        private readonly SyncProductsCommandHandler _syncProductsCommandHandler = new(EventLogRepository, SyncProductsRepository);

        [TestMethod]
        public async Task UpdateProductShouldSucceed()
        {
            // Create a category
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
                Images = ["Image" + Guid.NewGuid()],
            };

            await _createProductCommandHandler.Handle(createProductCommand, CancellationToken.None);

            var createdProduct = ShopDbContext.Products.Include(product => product.Images).FirstOrDefault(product => product.Name == createProductCommand.Name);

            var updateProductCommand = new UpdateProductCommand
            {
                Id = createdProduct!.Id,
                Name = "Updated product name" + Guid.NewGuid(),
                fileNames = ["UpdatedImage" + Guid.NewGuid()],
                CategoryId = createdCategory!.Id
            };

            await _updateProductCommandHandler.Handle(updateProductCommand, CancellationToken.None);

            var updatedProduct = ShopDbContext.Products.Include(product => product.Images).FirstOrDefault(product => product.Id == updateProductCommand.Id);

            Assert.IsNotNull(updatedProduct);
            Assert.AreEqual(updatedProduct.Name, updateProductCommand.Name);
        }

        [TestMethod]
        public async Task UpdateProductShouldModifyProductInMongoDatabaseAfterSync()
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


            var updateProductCommand = new UpdateProductCommand
            {
                Id = createdProduct!.Id,
                Name = "Updated product name" + Guid.NewGuid(),
                fileNames = ["UpdatedImage" + Guid.NewGuid()],
                CategoryId = createdCategory!.Id
            };

            await _updateProductCommandHandler.Handle(updateProductCommand, CancellationToken.None);

            await _syncProductsCommandHandler.Handle(new SyncProductsCommand(), CancellationToken.None);

            var updatedMongoProduct = await _getProductByIdQueryHandler.Handle(new GetProductByIdQuery { Id = updateProductCommand.Id }, CancellationToken.None);


            Assert.IsNotNull(updatedMongoProduct);
            Assert.AreEqual(updatedMongoProduct.Name, updateProductCommand.Name);
            Assert.IsTrue(updatedMongoProduct.Images.Count > 0);
        }

    }
}
