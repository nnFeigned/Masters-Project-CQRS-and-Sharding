using CQRS.Application.Categories.CommandHandlers;
using CQRS.Application.Categories.Commands;
using CQRS.Application.Categories.Queries;
using CQRS.Application.Categories.QueryHandlers;

namespace CQRS.Tests;

[TestClass]
public class DeleteCategoryCommandHandlerTests : BaseTests
{
    private readonly CreateCategoryCommandHandler _createCategoryCommandHandler = new(WriteCategoryRepository);
    private readonly DeleteCategoryCommandHandler _deleteCategoryCommandHandler = new(WriteCategoryRepository);
    private readonly SyncCategoriesCommandHandler _syncCategoriesCommandHandler = new(EventLogRepository, SyncCategoriesRepository);
    private readonly GetCategoryByIdQueryHandler _getCategoryByIdQueryHandler = new(ReadCategoryRepository);

    [TestMethod]
    public async Task DeleteCategoryShouldSucceed()
    {
        var createCategoryCommand = new CreateCategoryCommand
        {
            Name = "Test category" + Guid.NewGuid()
        };

        await _createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);

        var createdCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);
        Assert.IsNotNull(createdCategory);

        var deleteCategoryCommand = new DeleteCategoryCommand
        {
            Id = createdCategory.Id,
        };

        await _deleteCategoryCommandHandler.Handle(deleteCategoryCommand, CancellationToken.None);

        var deletedCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);

        Assert.IsNull(deletedCategory);
    }

    [TestMethod]
    public async Task DeleteCategoryShouldDeleteCategoryToMongoDatabaseAfterSync()
    {
        var createCategoryCommand = new CreateCategoryCommand
        {
            Name = "Test category" + Guid.NewGuid()
        };

        await _createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);

        var createdCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);
        Assert.IsNotNull(createdCategory);

        var deleteCategoryCommand = new DeleteCategoryCommand
        {
            Id = createdCategory.Id,
        };

        await _deleteCategoryCommandHandler.Handle(deleteCategoryCommand, CancellationToken.None);

        await _syncCategoriesCommandHandler.Handle(new SyncCategoriesCommand(), CancellationToken.None);

        var deletedMongoCategory = await _getCategoryByIdQueryHandler.Handle(new GetCategoryByIdQuery { Id = createdCategory.Id }, CancellationToken.None);

        Assert.IsNull(deletedMongoCategory);
    }
}