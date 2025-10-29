using Microsoft.AspNetCore.Mvc;
using NLayerArchitecture.Services.Products;

namespace NLayerArchitecture.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() => CreateActionResult(await productService.GetAllAsync());
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id) => CreateActionResult(await productService.GetByIdAsync(id));
    
    [HttpGet("top-price-product/{count}")]
    public async Task<IActionResult> GetTopPriceProductAsync(int count, int page) => CreateActionResult(await productService.GetTopPriceProductAsync(count));
    
    [HttpGet("pagination/{page:int}/{pageSize:int}")]
    public async Task<IActionResult> PaginationAsync(int page, int pageSize) => CreateActionResult(await productService.PaginationAsync(page, pageSize));
    
    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateProductRequest request) => CreateActionResult(await productService.AddAsync(request));
    
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(request));
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id) => CreateActionResult(await productService.DeleteAsync(id));
}