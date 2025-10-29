namespace NLayerArchitecture.Services.Products;

public record UpdateProductRequest(Guid Id, string Name, decimal Price, int Stock);