using Data;
using Handlers;
using Microsoft.Extensions.DependencyInjection;
using WarehouseConsole.Warehouses;

namespace WarehouseConsole;

public static class Injection
{
    public static IServiceCollection AddFromWarehouseConsole(this IServiceCollection services)
    {
        services.AddSingleton<Application>();
        
        services.AddSingleton<ProductsConsole>();
        services.AddSingleton<Warehouses.WarehouseConsole>();
        services.AddSingleton<ProductTypesConsole>();
        services.AddSingleton<ContainerConsole>();
        
        return services;
    }
}