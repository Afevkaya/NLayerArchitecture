using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Repositories.Products;

namespace NLayerArchitecture.Repositories;

public class NLayerArchitectureDbContext(DbContextOptions<NLayerArchitectureDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NLayerArchitectureDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}