using NLayerArchitecture.Repositories.Base;
using NLayerArchitecture.Repositories.Products;

namespace NLayerArchitecture.Repositories.Categories;

public class Category : BaseEntity<Guid>, IAuditEntity
{
    public string Name { get; set; } = default!;
    public ICollection<Product>? Products { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}