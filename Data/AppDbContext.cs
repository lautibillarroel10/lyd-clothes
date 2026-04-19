using Microsoft.EntityFrameworkCore;
using LYDClothes.Models;

namespace LYDClothes.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Datos de ejemplo para arrancar
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Name = "Remera Oversize Basic",
                Description = "100% algodón premium. Corte relajado, cuello redondo.",
                Price = 25000,
                Category = "Remeras",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1)
            },
            new Product
            {
                Id = 2,
                Name = "Hoodie Essential",
                Description = "Buzo con capucha. Tela gruesa, interior afelpado.",
                Price = 45000,
                Category = "Buzos",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1)
            },
            new Product
            {
                Id = 3,
                Name = "Pantalón Cargo Minimal",
                Description = "Cargo con bolsillos laterales. Tela ripstop liviana.",
                Price = 38000,
                Category = "Pantalones",
                IsActive = true,
                CreatedAt = new DateTime(2026, 1, 1)
            }
        );
    }
}
