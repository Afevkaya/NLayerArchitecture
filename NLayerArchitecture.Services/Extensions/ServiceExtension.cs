using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using NLayerArchitecture.Services.Categories;
using NLayerArchitecture.Services.ExceptionHandlers;
using NLayerArchitecture.Services.Products;
using NLayerArchitecture.Services.Products.Create;

namespace NLayerArchitecture.Services.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
        services.AddAutoMapper(cfg => { }, typeof(ServiceAssembly).Assembly);
        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
}