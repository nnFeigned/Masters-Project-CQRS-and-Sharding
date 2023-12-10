using MediatR;
using CQRS.Application.Commands;
using CQRS.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(string id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Post(string name, string? description)
    {
        var product = await _mediator.Send(new CreateProductCommand { Name = name, Description = description });
        return Ok(product);
    }


    [HttpPut]
    public async Task<IActionResult> Put(string id, string name, string description)
    {
        await _mediator.Send(new UpdateProductCommand { Id = id, Name = name, Description = description });
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _mediator.Send(new DeleteProductCommand { Id = id });
        return Ok();
    }
}