using Homework_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework_2.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(e =>
        {
            e.Property(p => p.Name).IsRequired().HasMaxLength(120);
            e.Property(p => p.Category).HasMaxLength(60);
            e.Property(p => p.Price).HasColumnType("decimal(10,2)");
            e.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        var seedDate = new DateTime(2024, 01, 01, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Martillo", Category = "Herramientas", Price = 350.00m, Stock = 20, CreatedAt = seedDate },
            new Product { Id = 2, Name = "Destornillador", Category = "Herramientas", Price = 150.00m, Stock = 35, CreatedAt = seedDate },
            new Product { Id = 3, Name = "Pintura Blanca 1L", Category = "Pinturas", Price = 520.00m, Stock = 15, CreatedAt = seedDate }
        );
    }
}
