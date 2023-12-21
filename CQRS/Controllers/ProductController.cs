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
        public async Task<ActionResult> GetProductAsync(GetProductByIdQuery model)
        {
            var product = await _mediator.Send(new GetProductByIdQuery { Id = model.Id });
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> PostProudct(CreateProductCommand model)
        {
            var product = await _mediator.Send(new CreateProductCommand { Name = model.Name, Description = model.Name });
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> PutProduct(UpdateProductCommand model)
        {
            await _mediator.Send(new UpdateProductCommand { Id = model.Id, Name = model.Name, Description = model.Description });
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand model)
        {
            await _mediator.Send(new DeleteProductCommand { Id = model.Id });
            return Ok();
        }
    }
}
