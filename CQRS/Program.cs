using CQRS.DataContext;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.Domain.Repository.Write;
using CQRS.MongoDB.Base;
using CQRS.MongoDB.Util;
using Microsoft.EntityFrameworkCore;
using System.Globalization;



var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(mongoDbSettings => builder.Configuration.GetSection("MongoDbSettings").Bind(mongoDbSettings));



var connectionString = builder.Configuration.GetConnectionString("ConnectionString");


Console.WriteLine($"Connecting to the database with connection string: {connectionString}");

builder.Services.AddScoped(MongoService.GetMongoCollection<Product>);
builder.Services.AddScoped(MongoService.GetMongoCollection<Category>);
builder.Services.AddScoped(MongoService.GetMongoCollection<Image>);

builder.Services.AddScoped(typeof(IReadRepository<>), typeof(MongoReadRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddDbContext<MagazineDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IWriteRepository<Category>, SQLWriteRepository<Category>>();
builder.Services.AddScoped<IWriteRepository<Product>, SQLWriteRepository<Product>>();
builder.Services.AddScoped<IWriteRepository<Image>, SQLWriteRepository<Image>>();


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();