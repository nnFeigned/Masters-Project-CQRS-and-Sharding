
using CQRS.Application.Photos.Commands;
using CQRS.Application.Photos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    public class ImageController : BaseController
    {
        public ImageController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult> GetCategories()
        {
            var products = await _mediator.Send(new GetAllImagesQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryAsync(string id)
        {
            var product = await _mediator.Send(new GetImageByIdQuery { Id = id });
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(string name)
        {
            var product = await _mediator.Send(new CreateImageCommand { FileName = name });
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(string id, string name)
        {
            await _mediator.Send(new UpdateImageCommand { Id = id, FileName = name });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _mediator.Send(new DeleteImageCommand { Id = id });
            return Ok();
        }
    }
}
