using NLayerArchitecture.Services.Categories.Create;
using NLayerArchitecture.Services.Categories.Dto;
using NLayerArchitecture.Services.Categories.Update;

namespace NLayerArchitecture.Services.Categories;

public interface ICategoryService
{
    Task<ServiceResult<CategoryWithProductsDto>> GetWithProducts(Guid id);
    Task<ServiceResult<List<CategoryWithProductsDto>>> GetAllWithProducts();
    Task<ServiceResult<CategoryDto>> GetByIdAsync(Guid id);
    Task<ServiceResult<List<CategoryDto>>> GetAllAsync();
    Task<ServiceResult<CreateCategoryResponse>> AddAsync(CreateCategoryRequest request);
    Task<ServiceResult<UpdateCategoryResponse>> UpdateAsync(UpdateCategoryRequest request);
    Task<ServiceResult> DeleteAsync(Guid id);
}