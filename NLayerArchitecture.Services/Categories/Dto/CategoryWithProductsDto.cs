using NLayerArchitecture.Services.Products;

namespace NLayerArchitecture.Services.Categories.Dto;

public record CategoryWithProductsDto(Guid Id, string Name, List<ProductDto> Products);