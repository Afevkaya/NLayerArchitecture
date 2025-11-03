using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using FluentValidation.AspNetCore;
using NLayerArchitecture.Services.Products;
using NLayerArchitecture.Services.Products.Create;

namespace NLayerArchitecture.Services.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();
        services.AddAutoMapper(cfg => { }, typeof(ServiceAssembly).Assembly);
        return services;
    }
}