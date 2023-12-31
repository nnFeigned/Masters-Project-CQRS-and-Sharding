using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Persistence.Context;

public class ShopDbContext : DbContext
{
    private readonly List<Type> _typesToTrack = [typeof(Category), typeof(Product)];

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<EventLog> EventLogs { get; set; }


    public ShopDbContext(DbContextOptions<ShopDbContext> options)
        : base(options)
    {
        this.Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>()
            .HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .HasForeignKey(product => product.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasMany(product => product.Images)
            .WithOne(image => image.Product)
            .HasForeignKey(image => image.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var entitiesToTrack = this.ChangeTracker.Entries()
            .Where(entityEntry => entityEntry.State != EntityState.Unchanged && entityEntry.State != EntityState.Detached)
            .Where(entityEntry => _typesToTrack.Contains(entityEntry.Entity.GetType()))
            .ToList();


        foreach (var entityEntry in entitiesToTrack)
        {
            var entity = (BaseEntity)entityEntry.Entity;
            var eventLog = new EventLog
            {
                Timestamp = DateTime.UtcNow,
                EntityType = entity.GetType().Name,
                ActionType = entityEntry.State.ToString(),
                Processed = false,
                EntityId = entity.Id
            };

            await this.EventLogs.AddAsync(eventLog, cancellationToken);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}