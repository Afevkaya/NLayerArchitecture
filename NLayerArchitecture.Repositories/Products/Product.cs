using NLayerArchitecture.Repositories.Categories;

namespace NLayerArchitecture.Repositories.Products;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = default!;
}