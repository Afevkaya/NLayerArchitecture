using Microsoft.EntityFrameworkCore;

namespace NLayerArchitecture.Repositories.Products;

public class ProductRepository(NLayerArchitectureDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
{
    public Task<List<Product>> GetTopPriceProductAsync(int count)
    {
        return _dbContext.Products
            .OrderByDescending(p => p.Price)
            .Take(count)
            .ToListAsync();
    }
}