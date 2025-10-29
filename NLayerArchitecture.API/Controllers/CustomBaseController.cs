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
        if (result.StatusCode == HttpStatusCode.NoContent)
            return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
        
        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
    }
    
    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.StatusCode == HttpStatusCode.NoContent)
            return new ObjectResult(null) { StatusCode = result.StatusCode.GetHashCode() };
        
        return new ObjectResult(result) { StatusCode = result.StatusCode.GetHashCode() };
    }
    
}