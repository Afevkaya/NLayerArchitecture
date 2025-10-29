namespace NLayerArchitecture.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}