using Data;
using Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace WarehouseApp;

public class Program
{
    public static void Main(string[] args)
    {
        var serviceProvider = new ServiceCollection()
            .AddFromWarehouseConsole()
            .AddFromData()
            .AddFromHandlers()
            .BuildServiceProvider();

        var app = serviceProvider.GetRequiredService<Application>();
        
        app.Run();
    }
}