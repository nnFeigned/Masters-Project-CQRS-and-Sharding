using CQRS.Domain.Entitites;
using CQRS.Domain.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;



namespace CQRS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly IProductRepository _productRepository;
        // GET: api/<ValuesController>

        public ProductController(IProductRepository productRepository, IMediator mediator) { 

            _mediator= mediator;
            _productRepository = productRepository; 
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            var product = await _productRepository.GetAllAsync();
            return Ok(product);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(string id)
        {
            var objectId = new ObjectId(id);

            var product = await _productRepository.GetProductByIdAsync(objectId);
            if (product is null)
            {
                return NotFound("User not found");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post(string name,string? description)
        {
            var product = new Product()
            {
                Name = name,
                Description = description
            };
            await _productRepository.AddProductAsync(product);


            return Ok(product);
        }


        [HttpPut]
        public async Task<IActionResult> Put(string ID, string name, string description)
        {
            var objectId = new ObjectId(ID);
            await _productRepository.UpdateProductAsync(objectId, name, description);
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _productRepository.DeleteProductAsync(new ObjectId(id));

            return Ok();
        }
    }
}
