using CQRS.Application.Products.Commands;
using CQRS.Application.Products.Queries;

using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    public class ProductController(IMediator mediator) : BaseController(mediator)
    {
        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var products = await Mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductAsync(GetProductByIdQuery getProductByIdQuery)
        {
            var product = await Mediator.Send(getProductByIdQuery);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductCommand createProductCommand)
        {
            var product = await Mediator.Send(createProductCommand);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProductCommand)
        {
            await Mediator.Send(updateProductCommand);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(DeleteProductCommand deleteProductCommand)
        {
            await Mediator.Send(deleteProductCommand);
            return Ok();
        }
    }
}
