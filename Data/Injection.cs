using Data.Repositories;
using Handlers.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class Injection
{
    public static IServiceCollection AddFromData(this IServiceCollection services)
    {
        services.AddSingleton<Database>();
        services.AddSingleton<IRepository, Repository>();
        
        return services;
    }
}