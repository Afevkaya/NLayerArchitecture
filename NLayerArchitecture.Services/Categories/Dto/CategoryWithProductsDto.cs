using NLayerArchitecture.Services.Products;

namespace NLayerArchitecture.Services.Categories.Dto;

public record CategoryWithProductsDto(Guid Id, string Name, DateTime CreatedDate, DateTime? UpdatedDate, List<ProductDto> Products);