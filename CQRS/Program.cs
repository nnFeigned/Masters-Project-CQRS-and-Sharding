using CQRS.Domain.Entities;
using CQRS.HostedServices;
using CQRS.Persistence.BaseRepositories;
using CQRS.Persistence.Context;
using CQRS.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(mongoDbSettings => builder.Configuration.GetSection("MongoDbSettings").Bind(mongoDbSettings));

var connectionString = builder.Configuration.GetConnectionString("ConnectionString");

Console.WriteLine($"Connecting to the database with connection string: {connectionString}");

builder.Services.AddScoped(MongoDbContext.GetMongoCollection<Product>);
builder.Services.AddScoped(MongoDbContext.GetMongoCollection<Category>);

builder.Services.AddScoped(typeof(IReadRepository<>), typeof(MongoReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(SqlWriteRepository<>));
builder.Services.AddScoped(typeof(ISyncRepository<>), typeof(SqlToMongoSyncRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddHostedService<SyncHostedService>();

builder.Services.AddDbContext<ShopDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString")));


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