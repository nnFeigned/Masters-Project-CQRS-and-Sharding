using CQRS.Application.Categories.Commands;
using CQRS.Application.Categories.Queries;

using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var categories = await Mediator.Send(new GetAllCategoryQuery());
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryAsync(GetCategoryByIdQuery getCategoryByIdQuery)
        {
            var category = await Mediator.Send(getCategoryByIdQuery);

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryCommand createCategoryCommand)
        {
            var category = await Mediator.Send(createCategoryCommand);
            return Ok(category);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(UpdateCategoryCommand updateCategoryCommand)
        {
            await Mediator.Send(updateCategoryCommand);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand deleteCategoryCommand)
        {
            await Mediator.Send(deleteCategoryCommand);
            return Ok();
        }
    }
}
