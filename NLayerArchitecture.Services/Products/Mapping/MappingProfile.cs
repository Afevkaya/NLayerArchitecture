using AutoMapper;
using NLayerArchitecture.Repositories.Products;
using NLayerArchitecture.Services.Products.Create;
using NLayerArchitecture.Services.Products.Update;

namespace NLayerArchitecture.Services.Products.Mapping;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
    }
}