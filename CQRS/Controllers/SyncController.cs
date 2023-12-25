using CQRS.Domain.Entities;
using CQRS.Persistence.Context;

using Microsoft.AspNetCore.Mvc;
using MediatR;

using Microsoft.EntityFrameworkCore;

using MongoDB.Driver;

namespace CQRS.Controllers;

[Route("api/[controller]")]
public class SyncController(
    IMediator mediator,
    ShopDbContext sqlContext,
    IMongoCollection<Category> categoriesCollection,
    IMongoCollection<Product> productsCollection) : BaseController(mediator)
{
    [HttpPost]
    public async Task<ActionResult> SyncFromSqlToMongo()
    {
        // this is just a hardcode - so don't do this in real life

        var sqlCategories = await sqlContext.Categories.AsNoTracking().ToListAsync();

        await categoriesCollection.DeleteManyAsync(category => true);
        await categoriesCollection.InsertManyAsync(sqlCategories);

        var sqlProducts = await sqlContext.Products
            .Include(product => product.Images)
            .AsNoTracking()
            .ToListAsync();

        await productsCollection.DeleteManyAsync(product => true);
        await productsCollection.InsertManyAsync(sqlProducts);

        return Ok();
    }
}