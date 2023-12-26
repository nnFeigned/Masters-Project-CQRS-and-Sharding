using CQRS.Application.Categories.CommandHandlers;
using CQRS.Application.Categories.Commands;
using CQRS.Application.Products.CommandHandlers;
using CQRS.Application.Products.Commands;
using CQRS.Application.Products.Queries;
using CQRS.Application.Products.QueryHandlers;

namespace CQRS.Tests
{
    [TestClass]
    public class CreateProductCommandHandlerTests : BaseTests
    {
        private readonly CreateProductCommandHandler _createProductCommandHandler = new(WriteProductRepository);
        private readonly SyncCategoriesCommandHandler _syncCategoriesCommandHandler = new(EventLogRepository, SyncCategoriesRepository);
        private readonly GetProductByIdQueryHandler _getProductByIdQueryHandler = new(ReadProductRepository);


        [TestMethod]
        public async Task CreateProductShouldAddProductToSqlDatabase()
        {
            var createProductCommand = new CreateProductCommand
            {
                Name = "Test product" + Guid.NewGuid()
            };

            await _createProductCommandHandler.Handle(createProductCommand, CancellationToken.None);

            var createdProduct = ShopDbContext.Categories.FirstOrDefault(product => product.Name == createProductCommand.Name);

            Assert.IsNotNull(createdProduct);
            Assert.AreEqual(createdProduct.Name, createProductCommand.Name);
        }

        [TestMethod]
        public async Task CreateProductShouldAddProductToMongoDatabaseAfterSync()
        {
            var createProductCommand = new CreateProductCommand
            {
                Name = "Test product" + Guid.NewGuid()
            };

            await _createProductCommandHandler.Handle(createProductCommand, CancellationToken.None);
            await _syncCategoriesCommandHandler.Handle(new SyncCategoriesCommand(), CancellationToken.None);

            var createdProduct = ShopDbContext.Categories.FirstOrDefault(product => product.Name == createProductCommand.Name);

            var createdMongoProduct = await _getProductByIdQueryHandler.Handle(new GetProductByIdQuery { Id = createdProduct!.Id }, CancellationToken.None);

            Assert.IsNotNull(createdMongoProduct);
            Assert.AreEqual(createdMongoProduct.Name, createdProduct.Name);
        }
    }
}