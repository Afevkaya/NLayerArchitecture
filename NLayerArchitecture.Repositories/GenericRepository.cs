using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Repositories.Base;

namespace NLayerArchitecture.Repositories;

public class GenericRepository<T,TId>(NLayerArchitectureDbContext dbContext) : IGenericRepository<T, TId> 
    where T : BaseEntity<TId> where TId : struct
{
    protected readonly NLayerArchitectureDbContext _dbContext = dbContext;
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public Task<bool> AnyAsync(TId id) => _dbSet.AnyAsync(x=>x.Id!.Equals(id));
    public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();

    public IQueryable<T> Where(Expression<Func<T, bool>> expression) => _dbSet.Where(expression).AsNoTracking();

    public async ValueTask<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}