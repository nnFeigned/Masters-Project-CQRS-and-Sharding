using CQRS.Application.Categories.CommandHandlers;
using CQRS.Application.Categories.Commands;
using CQRS.Application.Categories.Queries;
using CQRS.Application.Categories.QueryHandlers;

namespace CQRS.Tests
{
    [TestClass]
    public class CreateCategoryCommandHandlerTests : BaseTests
    {
        private readonly CreateCategoryCommandHandler _createCategoryCommandHandler = new(WriteCategoryRepository);
        private readonly SyncCategoriesCommandHandler _syncCategoriesCommandHandler = new(EventLogRepository, SyncCategoriesRepository);
        private readonly GetCategoryByIdQueryHandler _getCategoryByIdQueryHandler = new(ReadCategoryRepository);


        [TestMethod]
        public async Task CreateCategoryShouldAddCategoryToSqlDatabase()
        {
            var createCategoryCommand = new CreateCategoryCommand
            {
                Name = "Test category" + Guid.NewGuid()
            };

            await _createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);

            var createdCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);

            Assert.IsNotNull(createdCategory);
            Assert.AreEqual(createdCategory.Name, createCategoryCommand.Name);
        }

        [TestMethod]
        public async Task CreateCategoryShouldAddCategoryToMongoDatabaseAfterSync()
        {
            var createCategoryCommand = new CreateCategoryCommand
            {
                Name = "Test category" + Guid.NewGuid()
            };

            await _createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);
            await _syncCategoriesCommandHandler.Handle(new SyncCategoriesCommand(), CancellationToken.None);

            var createdCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);

            var createdMongoCategory = await _getCategoryByIdQueryHandler.Handle(new GetCategoryByIdQuery { Id = createdCategory!.Id }, CancellationToken.None);

            Assert.IsNotNull(createdMongoCategory);
            Assert.AreEqual(createdMongoCategory.Name, createdCategory.Name);
        }
    }
}