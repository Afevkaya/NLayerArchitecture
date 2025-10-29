namespace NLayerArchitecture.Services.Products;

public interface IProductService
{
    Task<ServiceResult<ProductDto>> GetByIdAsync(Guid id);
    Task<ServiceResult<List<ProductDto>>> GetAllAsync();
    Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count);
    Task<ServiceResult<CreateProductResponse>> AddAsync(CreateProductRequest request);
    Task<ServiceResult<UpdateProductResponse>> UpdateAsync(UpdateProductRequest request);
    Task<ServiceResult> DeleteAsync(Guid id);
}