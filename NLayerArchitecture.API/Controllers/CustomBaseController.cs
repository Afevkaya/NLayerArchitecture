using System.Net;
using Microsoft.AspNetCore.Mvc;
using NLayerArchitecture.Services;

namespace NLayerArchitecture.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomBaseController : ControllerBase
{
    
    [NonAction]
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.Created when result.UrlAsCreated is not null => new CreatedResult(result.UrlAsCreated, result),
            _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() }
        };
    }
    
    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        return result.StatusCode switch
        {
            HttpStatusCode.NoContent => new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() },
            _ => new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() }
        };
    }
    
}