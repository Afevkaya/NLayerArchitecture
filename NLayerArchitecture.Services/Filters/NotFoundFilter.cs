using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayerArchitecture.Repositories;

namespace NLayerArchitecture.Services.Filters;

public class NotFoundFilter<T,TId>(IGenericRepository<T,TId> genericRepository): Attribute, IAsyncActionFilter 
    where T : class where TId : struct
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var args = context.ActionArguments;
        if (args.Count == 0)
        {
            await next();
            return;
        }

        object? idObj;
        if (args.TryGetValue("id", out var val) || args.TryGetValue($"{typeof(T).Name}Id", out val)) idObj = val;
        else
            idObj = args.Values.FirstOrDefault(v => v is TId);

        if (idObj is not TId id)
        {
            await next();
            return;
        }

        var anyEntity = await genericRepository.AnyAsync(id);
        if (!anyEntity)
        {
            var entityName = typeof(T).Name;
            var actionMethodName = context.ActionDescriptor.RouteValues["action"];
            var result = ServiceResult.Failed($"veri bulunamamıştır. ({entityName}) - ({actionMethodName})");
            context.Result = new NotFoundObjectResult(result);
            return;
        }

        await next();
    }

}