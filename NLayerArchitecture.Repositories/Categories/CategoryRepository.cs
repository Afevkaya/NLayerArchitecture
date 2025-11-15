using Microsoft.EntityFrameworkCore;

namespace NLayerArchitecture.Repositories.Categories;

public class CategoryRepository(NLayerArchitectureDbContext dbContext) :GenericRepository<Category>(dbContext), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProductsAsync(Guid id)
    {
        return _dbContext.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
    }

    public IQueryable<Category?> GetCategoriesWithProducts()
    {
        return _dbContext.Categories.Include(c => c.Products).AsQueryable();
    }
}