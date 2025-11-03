using System.Net;
using System.Text.Json.Serialization;

namespace NLayerArchitecture.Services;

// Static Factory Method Pattern
public class ServiceResult<T>
{
    public T? Data { get; set; }
    public List<string>? ErrorMessages { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
    [JsonIgnore] public bool IsFailed => !IsSuccess;
    [JsonIgnore] public HttpStatusCode? StatusCode { get; set; }
    [JsonIgnore] public string? UrlAsCreated { get; set; }

    public static ServiceResult<T> Success(T data, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult<T> { Data = data, StatusCode = statusCode };
    }
    public static ServiceResult<T> SuccessAsCreated(T data, string urlAsCreated)
    {
        return new ServiceResult<T> { Data = data, UrlAsCreated = urlAsCreated, StatusCode = HttpStatusCode.Created };
    }
    
    public static ServiceResult<T> Failed(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T> { ErrorMessages = errorMessages, StatusCode = statusCode };
    }
    
    public static ServiceResult<T> Failed(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult<T> { ErrorMessages = [errorMessage], StatusCode = statusCode };
    }
}

public class ServiceResult
{
    public List<string>? ErrorMessages { get; set; }
    [JsonIgnore] public bool IsSuccess => ErrorMessages == null || ErrorMessages.Count == 0;
    [JsonIgnore] public bool IsFailed => !IsSuccess;
    [JsonIgnore] public HttpStatusCode? StatusCode { get; set; }
    
    public static ServiceResult Success(HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        return new ServiceResult{ StatusCode = statusCode };
    }
    
    public static ServiceResult Failed(List<string> errorMessages, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult{ ErrorMessages = errorMessages, StatusCode = statusCode };
    }
    
    public static ServiceResult Failed(string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new ServiceResult { ErrorMessages = [errorMessage], StatusCode = statusCode };
    }
}