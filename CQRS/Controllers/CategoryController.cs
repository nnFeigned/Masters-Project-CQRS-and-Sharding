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
        public async Task<ActionResult> GetCategoryAsync(string id)
        {
            var product = await _mediator.Send(new GetCategoryByIdQuery { Id = id });
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(string name, List<ObjectId> listProductId)
        {
            var product = await _mediator.Send(new CreateCategoryCommand { Name = name, Products = listProductId });
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(string id, string name, List<ObjectId> listProductId)
        {
            await _mediator.Send(new UpdateCategoryCommand { Id = id, Name = name, Products = listProductId });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _mediator.Send(new DeleteCategoryCommand { Id = id });
            return Ok();
        }
    }
}
