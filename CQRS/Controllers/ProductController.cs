using CQRS.Application.Production.Commands;
using CQRS.Application.Production.Queries;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        public ProductController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductAsync(string id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostProudct(string name, string? description)
        {
            var product = await _mediator.Send(new CreateProductCommand { Name = name, Description = description });
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(string id, string name, string description)
        {
            await _mediator.Send(new UpdateProductCommand { Id = id, Name = name, Description = description });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            await _mediator.Send(new DeleteProductCommand { Id = id });
            return Ok();
        }
    }
}
