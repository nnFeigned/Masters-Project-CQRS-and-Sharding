using CQRS.Application.Categories.Commands;
using CQRS.Application.Categories.Queries;
using CQRS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        public CategoryController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var products = await _mediator.Send(new GetAllCategoryQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryAsync(GetCategoryByIdQuery model)
        {
            var product = await _mediator.Send(new GetCategoryByIdQuery { Id = model.Id });
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(CreateCategoryCommand model)
        {
            var product = await _mediator.Send(new CreateCategoryCommand { Name = model.Name, Products = model.Products });
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(UpdateCategoryCommand model)
        {
            await _mediator.Send(new UpdateCategoryCommand { Id = model.Id, Name = model.Name, Products = model.Products });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand model)
        {
            await _mediator.Send(new DeleteCategoryCommand { Id = model.Id });
            return Ok();
        }
    }
}
