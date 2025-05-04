using Data;
using Handlers;
using Microsoft.Extensions.DependencyInjection;
using WarehouseApp.Warehouses;

namespace WarehouseApp;

public static class Injection
{
    public static IServiceCollection AddFromWarehouseConsole(this IServiceCollection services)
    {
        services.AddSingleton<Application>();
        
        services.AddSingleton<ProductsConsole>();
        services.AddSingleton<WarehouseConsole>();
        services.AddSingleton<ProductTypesConsole>();
        services.AddSingleton<ContainerConsole>();
        
        return services;
    }
}