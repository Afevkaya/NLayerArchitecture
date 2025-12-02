namespace NLayerArchitecture.Repositories.Categories;

public interface ICategoryRepository:IGenericRepository<Category,Guid>
{
    Task<Category?> GetCategoryWithProductsAsync(Guid id);
    IQueryable<Category?> GetCategoriesWithProducts();
}