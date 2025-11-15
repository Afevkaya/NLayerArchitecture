namespace NLayerArchitecture.Services.Products.Update;

public record UpdateProductRequest(Guid Id, string Name, decimal Price, int Stock);