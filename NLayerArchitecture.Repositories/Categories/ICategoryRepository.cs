namespace NLayerArchitecture.Repositories.Categories;

public interface ICategoryRepository:IGenericRepository<Category>
{
    Task<Category?> GetCategoryWithProductsAsync(Guid id);
    IQueryable<Category?> GetCategoriesWithProducts();
}