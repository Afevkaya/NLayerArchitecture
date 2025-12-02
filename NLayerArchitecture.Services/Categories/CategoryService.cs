using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Repositories;
using NLayerArchitecture.Repositories.Categories;
using NLayerArchitecture.Services.Categories.Create;
using NLayerArchitecture.Services.Categories.Dto;
using NLayerArchitecture.Services.Categories.Update;

namespace NLayerArchitecture.Services.Categories;

public class CategoryService(ICategoryRepository categoryRepository, IMapper mapper, IUnitOfWork unitOfWork): ICategoryService
{
    public async Task<ServiceResult<CategoryWithProductsDto>> GetWithProducts(Guid id)
    {
        var category = await categoryRepository.GetCategoryWithProductsAsync(id);
        if (category == null)
            ServiceResult<Category>.Failed("Kategori bulunamadı",HttpStatusCode.NotFound);
        
        var categoryAsDto = mapper.Map<CategoryWithProductsDto>(category);
        return ServiceResult<CategoryWithProductsDto>.Success(categoryAsDto, HttpStatusCode.OK);
    }
    public async Task<ServiceResult<List<CategoryWithProductsDto>>> GetAllWithProducts()
    {
        var categories = await categoryRepository.GetCategoriesWithProducts().ToListAsync();
        if (categories.Count == 0)
            ServiceResult<List<CategoryWithProductsDto>>.Failed("Kategoriler bulunamadı",HttpStatusCode.NotFound);
        
        var categoriesAsDto = mapper.Map<List<CategoryWithProductsDto>>(categories);
        return ServiceResult<List<CategoryWithProductsDto>>.Success(categoriesAsDto,HttpStatusCode.OK);
    }
    public async Task<ServiceResult<CategoryDto>> GetByIdAsync(Guid id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null)
            ServiceResult<CategoryDto>.Failed("Kategori bulunamadı",HttpStatusCode.NotFound);
        
        var categoryAsDto = mapper.Map<CategoryDto>(category);
        return ServiceResult<CategoryDto>.Success(categoryAsDto,HttpStatusCode.OK);
    }
    public async Task<ServiceResult<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await categoryRepository.GetAll().ToListAsync();
        if (categories.Count == 0)
            ServiceResult<List<CategoryDto>>.Failed("Kategoriler bulunamadı",HttpStatusCode.NotFound);
        var categoriesAsDto = mapper.Map<List<CategoryDto>>(categories);
        return ServiceResult<List<CategoryDto>>.Success(categoriesAsDto);
    }  
    public async Task<ServiceResult<CreateCategoryResponse>> AddAsync(CreateCategoryRequest request)
    {
        if (request is null)
            ServiceResult<CreateCategoryResponse>.Failed("Model hatalı");
        
        var anyCategory = await categoryRepository.Where(c=>c.Name == request!.Name).AnyAsync();
        if (anyCategory)
            return ServiceResult<CreateCategoryResponse>.Failed("Kategori zaten mevcut",HttpStatusCode.BadRequest);
        
        var category = mapper.Map<Category>(request);
        await categoryRepository.AddAsync(category);
        var response = await unitOfWork.SaveChangesAsync();
        return response < 0 
            ? ServiceResult<CreateCategoryResponse>.Failed("Kategori eklenemedi",HttpStatusCode.InternalServerError) 
            : ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id),$"api/categories/{category.Id}");
    }
    public async Task<ServiceResult<UpdateCategoryResponse>> UpdateAsync(Guid id, UpdateCategoryRequest request)
    {
        if (request is null)
            ServiceResult<UpdateCategoryResponse>.Failed("Model hatalı");
        
        var isCategoryNameExist = await categoryRepository.Where(c=>c.Name == request!.Name && c.Id != id).AnyAsync();
        if (isCategoryNameExist)
            return ServiceResult<UpdateCategoryResponse>.Failed("Kategori ismi zaten mevcut",HttpStatusCode.BadRequest);
        
        var category = mapper.Map<Category>(request);
        category.Id = id;
        await unitOfWork.SaveChangesAsync();
        return ServiceResult<UpdateCategoryResponse>.Success(new UpdateCategoryResponse(category!.Id));
    }
    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
        if(id == Guid.Empty)
            ServiceResult.Failed("Id hatalı");

        var category = await categoryRepository.GetByIdAsync(id);
        
        categoryRepository.Delete(category!);
        await unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}