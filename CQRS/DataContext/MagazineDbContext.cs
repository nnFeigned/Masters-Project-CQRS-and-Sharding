using CQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace CQRS.DataContext
{
    

    public class MagazineDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Image> Images { get; set; }

        public MagazineDbContext(DbContextOptions<MagazineDbContext> options)
            : base(options)
        {
            Console.WriteLine("Alele");
            IsDatabaseConnected();
            Console.WriteLine("Some tesxt");
        }


        public bool IsDatabaseConnected()
        {
            try
            {
                return Database.CanConnect();
            }
            catch
            {
                return false;
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Image>().ToTable("Images");


            modelBuilder.Entity<Category>()
                .HasMany(c => c.ProductIds)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Images)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId);
        }


    }

}
