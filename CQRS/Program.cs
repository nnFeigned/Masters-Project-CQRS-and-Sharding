using CQRS.DataContext;
using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.MongoDB;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(mongoDbSettings => builder.Configuration.GetSection("MongoDbSettings").Bind(mongoDbSettings));

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

Console.WriteLine($"Connecting to the database with connection string: {connectionString}");

builder.Services.AddScoped(MongoDbContext.GetMongoCollection<Product>);
builder.Services.AddScoped(MongoDbContext.GetMongoCollection<Category>);

builder.Services.AddScoped(typeof(IReadRepository<>), typeof(MongoReadRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));


builder.Services.AddDbContext<ShopDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IWriteRepository<Category>, SqlWriteRepository<Category>>();
builder.Services.AddScoped<IWriteRepository<Product>, SqlWriteRepository<Product>>();
builder.Services.AddScoped<IWriteRepository<Image>, SqlWriteRepository<Image>>();

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