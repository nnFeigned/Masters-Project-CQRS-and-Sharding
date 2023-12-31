using CQRS.Application.Products.Commands;
using CQRS.Application.Products.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CQRS.Controllers;

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
    public async Task<ActionResult> GetProductAsync(Guid id)
    {

        var product = await Mediator.Send(new GetProductByIdQuery() { Id=id });

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
    public async Task<IActionResult> DeleteProduct(Guid id)
    {
        await Mediator.Send(new DeleteProductCommand() { Id=id });
        return Ok();
    }
}