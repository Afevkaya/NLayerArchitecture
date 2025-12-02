
namespace NLayerArchitecture.Repositories.Products;

public interface IProductRepository: IGenericRepository<Product,Guid>
{
    Task<List<Product>> GetTopPriceProductAsync(int count);
    Task<List<Product>> PaginationAsync(int page, int pageSize);
}