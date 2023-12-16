using CQRS.Domain.Entities;
using CQRS.Domain.Repository;
using CQRS.MongoDB.Base;
using CQRS.MongoDB.Util;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDBSettings>(mongoDbSettings => builder.Configuration.GetSection("MongoDbSettings").Bind(mongoDbSettings));



builder.Services.AddScoped(sp => MongoService.GetMongoCollection<Product>(sp));
builder.Services.AddScoped(sp => MongoService.GetMongoCollection<Category>(sp));
builder.Services.AddScoped(sp => MongoService.GetMongoCollection<Image>(sp));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddScoped(typeof(IReadRepository<>), typeof(MongoReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(SQLWriteRepository<>));




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