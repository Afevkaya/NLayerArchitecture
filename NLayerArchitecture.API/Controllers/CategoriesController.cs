using Microsoft.AspNetCore.Mvc;
using NLayerArchitecture.Repositories.Categories;
using NLayerArchitecture.Repositories.Products;
using NLayerArchitecture.Services.Categories;
using NLayerArchitecture.Services.Categories.Create;
using NLayerArchitecture.Services.Categories.Update;
using NLayerArchitecture.Services.Filters;

namespace NLayerArchitecture.API.Controllers;

public class CategoriesController(ICategoryService categoryService): CustomBaseController
{
    [HttpGet("products")]
    public async Task<IActionResult> GetAllWithProducts() => CreateActionResult(await categoryService.GetAllWithProducts());
    
    [HttpGet("{id:guid}/products")]
    public async Task<IActionResult> GetWithProducts(Guid id) => CreateActionResult(await categoryService.GetWithProducts(id));
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() => CreateActionResult(await categoryService.GetAllAsync());
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id) => CreateActionResult(await categoryService.GetByIdAsync(id));
    
    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateCategoryRequest request) => CreateActionResult(await categoryService.AddAsync(request));
    
    [ServiceFilter(typeof(NotFoundFilter<Category, Guid>))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id,UpdateCategoryRequest request) => CreateActionResult(await categoryService.UpdateAsync(id, request));
    
    [ServiceFilter(typeof(NotFoundFilter<Category, Guid>))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id) => CreateActionResult(await categoryService.DeleteAsync(id));
}