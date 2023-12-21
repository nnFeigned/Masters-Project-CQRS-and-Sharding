
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
        public async Task<ActionResult> GetCategoryAsync(GetImageByIdQuery model)
        {
            var product = await _mediator.Send(new GetImageByIdQuery { Id = model.Id });
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostCategory(CreateImageCommand model)
        {
            var product = await _mediator.Send(new CreateImageCommand { FileName = model.FileName });
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> PutCategory(UpdateImageCommand model)
        {
            await _mediator.Send(new UpdateImageCommand { Id = model.Id, FileName = model.FileName });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(DeleteImageCommand model)
        {
            await _mediator.Send(new DeleteImageCommand { Id = model.Id });
            return Ok();
        }
    }
}
