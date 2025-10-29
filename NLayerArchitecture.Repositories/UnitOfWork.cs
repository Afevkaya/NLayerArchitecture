namespace NLayerArchitecture.Repositories;

public class UnitOfWork(NLayerArchitectureDbContext dbContext):IUnitOfWork
{
    public Task<int> SaveChangesAsync() => dbContext.SaveChangesAsync();
}