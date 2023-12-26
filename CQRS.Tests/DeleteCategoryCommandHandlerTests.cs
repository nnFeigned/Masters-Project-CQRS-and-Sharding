using CQRS.Application.Categories.CommandHandlers;
using CQRS.Application.Categories.Commands;

namespace CQRS.Tests;

[TestClass]
public class DeleteCategoryCommandHandlerTests : BaseTests
{
    private readonly CreateCategoryCommandHandler _createCategoryCommandHandler = new(WriteCategoryRepository);
    private readonly DeleteCategoryCommandHandler _deleteCategoryCommandHandler = new(WriteCategoryRepository);

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
            Id = createdCategory!.Id,
        };

        await _deleteCategoryCommandHandler.Handle(deleteCategoryCommand, CancellationToken.None);

        var deletedCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);

        Assert.IsNull(deletedCategory);
    }
}