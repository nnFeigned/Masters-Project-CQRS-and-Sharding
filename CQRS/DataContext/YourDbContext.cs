using CQRS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CQRS.DataContext
{
    

    public class YourDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        public YourDbContext(DbContextOptions<YourDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

           /* modelBuilder.Entity<Product>()
                .HasOne(p => p.CategoryId.ToString())
                .WithMany(c => c.Id)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Image>()
                .HasOne(i => i.Id.ToString())
                .WithMany()
                .HasForeignKey(i => i.ProductId);


            base.OnModelCreating(modelBuilder);\
           */
        }
    }

}
