using Handlers.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Handlers;

public static class Injection
{
    public static IServiceCollection AddFromHandlers(this IServiceCollection services)
    {
        var assembly = typeof(Injection).Assembly;
        
        services.Scan(scan => scan.FromAssemblies(assembly)
            .AddClasses(classes => classes.AssignableToAny(
                typeof(ICommandHandler<,>),
                typeof(IHandler<>)))
            .AsSelfWithInterfaces()
            .WithLifetime(ServiceLifetime.Scoped));

        return services;
    }
}