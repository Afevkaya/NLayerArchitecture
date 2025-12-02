using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NLayerArchitecture.Repositories.Base;

namespace NLayerArchitecture.Repositories.Interceptors;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
    {
        { EntityState.Added, AddBehavior },
        { EntityState.Modified, ModifiedBehavior }
    };
    private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.CreatedDate = DateTime.UtcNow;
        context.Entry(auditEntity).Property(x => x.UpdatedDate).IsModified = false;
    }
    
    private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.UpdatedDate = DateTime.UtcNow;
        context.Entry(auditEntity).Property(x => x.CreatedDate).IsModified = false;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
        {
            if(entityEntry.Entity is not IAuditEntity auditEntity) continue;

            #region Yöntemler

            /*
            switch (entityEntry.State)
            {
                #region 1.yol
                // case EntityState.Added:
                //     auditEntity.CreatedDate = DateTime.UtcNow;
                //     eventData.Context.Entry(auditEntity).Property(x => x.UpdatedDate).IsModified = false;
                //     break;
                // case EntityState.Modified:
                //     auditEntity.UpdatedDate = DateTime.UtcNow;
                //     eventData.Context.Entry(auditEntity).Property(x => x.CreatedDate).IsModified = false;
                //     break;
                #endregion

                #region 2.yol
                // case EntityState.Added:
                //     AddBehavior(eventData.Context, auditEntity);
                //     break;
                // case EntityState.Modified:
                //     ModifiedBehavior(eventData.Context, auditEntity);
                //     break;
                #endregion
            }
            */

            #endregion
            
            #region 3.yol

            if(entityEntry.State is not (EntityState.Added or EntityState.Modified)) continue;
            Behaviors[entityEntry.State](eventData.Context, auditEntity);

            #endregion
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}