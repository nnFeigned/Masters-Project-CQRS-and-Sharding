using CQRS.Application.Categories.CommandHandlers;
using CQRS.Application.Categories.Commands;

namespace CQRS.Tests;

[TestClass]
public class UpdateCategoryCommandHandlerTests : BaseTests
{
    private readonly CreateCategoryCommandHandler _createCategoryCommandHandler = new(WriteCategoryRepository);
    private readonly UpdateCategoryCommandHandler _updateCategoryCommandHandler = new(WriteCategoryRepository);

    [TestMethod]
    public async Task UpdateCategoryShouldSucceed()
    {
        var createCategoryCommand = new CreateCategoryCommand
        {
            Name = "Test category" + Guid.NewGuid()
        };

        await _createCategoryCommandHandler.Handle(createCategoryCommand, CancellationToken.None);

        var createdCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == createCategoryCommand.Name);

        var updateCategoryCommand = new UpdateCategoryCommand
        {
            Id = createdCategory!.Id,
            Name = "Test category updated" + Guid.NewGuid()
        }; 

        await _updateCategoryCommandHandler.Handle(updateCategoryCommand, CancellationToken.None);

        var updatedCategory = ShopDbContext.Categories.FirstOrDefault(category => category.Name == updateCategoryCommand.Name);

        Assert.IsNotNull(updatedCategory);
        Assert.AreEqual(updatedCategory.Name, updateCategoryCommand.Name);
    }
}