namespace NLayerArchitecture.Repositories.Base;

public interface IAuditEntity
{
    DateTime CreatedDate { get; set; }
    DateTime? UpdatedDate { get; set; }
}