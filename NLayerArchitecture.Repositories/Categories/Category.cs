using NLayerArchitecture.Repositories.Base;
using NLayerArchitecture.Repositories.Products;

namespace NLayerArchitecture.Repositories.Categories;

public class Category:IAuditEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public ICollection<Product>? Products { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}