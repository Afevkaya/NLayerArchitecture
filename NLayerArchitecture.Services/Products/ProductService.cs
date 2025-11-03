using System.Net;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Repositories;
using NLayerArchitecture.Repositories.Products;
using NLayerArchitecture.Services.Products.Create;
using NLayerArchitecture.Services.Products.Update;

namespace NLayerArchitecture.Services.Products;

public class ProductService(IProductRepository productRepository, IUnitOfWork unitOfWork,IMapper mapper):IProductService
{
    public async Task<ServiceResult<List<ProductDto>>> GetAllAsync()
    {
        var products = await productRepository.GetAll().ToListAsync();
        var productDto = products.Select(mapper.Map<ProductDto>).ToList();
        return ServiceResult<List<ProductDto>>.Success(productDto);
    }
    public async Task<ServiceResult<ProductDto>> GetByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
            return ServiceResult<ProductDto>.Failed("Product is not found", HttpStatusCode.NotFound);
        
        var productDto = mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto>.Success(productDto);
    }
    public async Task<ServiceResult<List<ProductDto>>> GetTopPriceProductAsync(int count)
    {
        var products = await productRepository.GetTopPriceProductAsync(count);

        var productDto = products.Select(mapper.Map<ProductDto>).ToList();

        return ServiceResult<List<ProductDto>>.Success(productDto);
    }
    public async Task<ServiceResult<CreateProductResponse>> AddAsync(CreateProductRequest request)
    {
        var anyProduct = await productRepository.Where(p=>p.Name == request.Name).AnyAsync();
        if (anyProduct)
            return ServiceResult<CreateProductResponse>.Failed("Ürün ismi bulunaktadır", HttpStatusCode.BadRequest);
        
        // var product = new Product
        // {
        //     Id = Guid.NewGuid(),
        //     Name = request.Name,
        //     Price = request.Price,
        //     Stock = request.Stock
        // };
        var product = mapper.Map<Product>(request);
        await productRepository.AddAsync(product) ;
        var result = await unitOfWork.SaveChangesAsync();
        return result > 0 
            ? ServiceResult<CreateProductResponse>.SuccessAsCreated(new CreateProductResponse(product.Id),$"api/products/{product.Id}") 
            : ServiceResult<CreateProductResponse>.Failed("Product is not added");
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
       mapper.Map(request,product);
        
        productRepository.Update(product);
        await unitOfWork.SaveChangesAsync();
        
        return ServiceResult<UpdateProductResponse>.Success(new UpdateProductResponse(product.Id));
    }
    public async Task<ServiceResult<UpdateProductResponse>> UpdateStockAsync(UpdateProductStockRequest request)
    {
        if(request.Id == Guid.Empty)
            return ServiceResult<UpdateProductResponse>.Failed("Product Id is required");
        
        var product = await productRepository.GetByIdAsync(request.Id);
        if (product == null)
            return ServiceResult<UpdateProductResponse>.Failed("Product is not found",HttpStatusCode.NotFound);
        
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

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult<List<ProductDto>>> PaginationAsync(int page, int pageSize)
    {
        var paginationData = await productRepository.PaginationAsync(page, pageSize);
        if (paginationData.Count == 0)
            return ServiceResult<List<ProductDto>>.Failed("No products found for the given page and page size", HttpStatusCode.NotFound);
        
        var productDto = paginationData.Select(mapper.Map<ProductDto>).ToList();
        return ServiceResult<List<ProductDto>>.Success(productDto);
    }
}