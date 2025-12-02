using Microsoft.AspNetCore.Mvc;
using NLayerArchitecture.Repositories.Products;
using NLayerArchitecture.Services.Filters;
using NLayerArchitecture.Services.Products;
using NLayerArchitecture.Services.Products.Create;
using NLayerArchitecture.Services.Products.Update;
using NLayerArchitecture.Services.Products.UpdateStock;

namespace NLayerArchitecture.API.Controllers;

public class ProductsController(IProductService productService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllAsync() => CreateActionResult(await productService.GetAllAsync());
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id) => CreateActionResult(await productService.GetByIdAsync(id));
    
    [HttpGet("top-price-product/{count:int}")]
    public async Task<IActionResult> GetTopPriceProductAsync(int count) => CreateActionResult(await productService.GetTopPriceProductAsync(count));
    
    [HttpGet("pagination/{page:int}/{pageSize:int}")]
    public async Task<IActionResult> PaginationAsync(int page, int pageSize) => CreateActionResult(await productService.PaginationAsync(page, pageSize));
    
    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateProductRequest request) => CreateActionResult(await productService.AddAsync(request));
    
    [ServiceFilter(typeof(NotFoundFilter<Product, Guid>))]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateProductRequest request) => CreateActionResult(await productService.UpdateAsync(id, request));
    
    [HttpPatch("stock")]
    public async Task<IActionResult> PatchAsync(UpdateProductStockRequest request) => CreateActionResult(await productService.UpdateStockAsync(request));
    
    [ServiceFilter(typeof(NotFoundFilter<Product, Guid>))]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id) => CreateActionResult(await productService.DeleteAsync(id));
}