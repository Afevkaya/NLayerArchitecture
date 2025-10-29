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

    public Task<List<Product>> PaginationAsync(int page, int pageSize)
    {
        return GetAll()
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}