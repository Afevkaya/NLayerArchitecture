namespace NLayerArchitecture.Repositories.Base;

public class BaseEntity<T>
{
    public T Id { get; set; } = default!;
}