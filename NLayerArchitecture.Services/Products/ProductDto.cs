namespace NLayerArchitecture.Services.Products;

public record ProductDto(Guid Id, string Name, decimal Price, int Stock);