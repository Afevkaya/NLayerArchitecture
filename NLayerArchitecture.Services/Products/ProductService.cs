using System.Net;
using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Repositories;
using NLayerArchitecture.Repositories.Products;

namespace NLayerArchitecture.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork):IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();
        return ServiceResult<List<ProductDto>>.Success(productDto);
    }
    public async Task<ServiceResult<ProductDto>> GetByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
            return ServiceResult<ProductDto>.Failed("Product is not found", HttpStatusCode.NotFound);
        
        var productDto = new ProductDto(product.Id, product.Name, product.Price, product.Stock);
        return ServiceResult<ProductDto>.Success(productDto);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductAsync(count);

        var productDto = products.Select(p => new ProductDto(p.Id, p.Name, p.Price, p.Stock)).ToList();

        return ServiceResult<List<ProductDto>>.Success(productDto);
    }
    public async Task<ServiceResult<CreateProductResponse>> AddAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Price = request.Price,
            Stock = request.Stock
        };
        await productRepository.AddAsync(product) ;
        var result = await unitOfWork.SaveChangesAsync();
        return result > 0 ? ServiceResult<CreateProductResponse>.Success(new CreateProductResponse(product.Id)) : ServiceResult<CreateProductResponse>.Failed("Product is not added");
    }
    public async Task<ServiceResult<UpdateProductResponse>> UpdateAsync(UpdateProductRequest request)
    {
        if(request.Id == Guid.Empty)
            return ServiceResult<UpdateProductResponse>.Failed("Product Id is required");
        
        var product = await productRepository.GetByIdAsync(request.Id);
        if (product == null)
            return ServiceResult<UpdateProductResponse>.Failed("Product is not found",HttpStatusCode.NotFound);
        
        product.Name = request.Name;
        product.Price = request.Price;
        product.Stock = request.Stock;
        
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        
        return ServiceResult<UpdateProductResponse>.Success(new UpdateProductResponse(product.Id));
    }
    public async Task<ServiceResult> DeleteAsync(Guid id)
    {
        // Fast Fail
        // Guard Clauses
        
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
            return ServiceResult.Failed("Product is not found", HttpStatusCode.NotFound);
        
        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();

        return ServiceResult.Success();
    }
}