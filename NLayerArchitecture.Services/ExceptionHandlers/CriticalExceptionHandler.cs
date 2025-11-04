using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace NLayerArchitecture.Services.ExceptionHandlers;

public class CriticalExceptionHandler:IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if(exception is CriticalException)
            Console.WriteLine("Kritik bir hata oluştu. Yetkili kişilere bildirilmesi gerekiyor.");
        return ValueTask.FromResult(false);
    }
}