using System.Drawing;
using System.Threading.Channels;
using Data;
using Entities.Clients;
using Handlers;
using Handlers.Products.Create;
using Handlers.Products.Get;
using Handlers.ProductTypes.Create;
using Handlers.ProductTypes.Get;
using Handlers.Warehouses.Create;
using Handlers.Warehouses.Get;
using Microsoft.Extensions.DependencyInjection;

namespace WarehouseConsole;

public class Program
{
    private readonly WarehouseConsole _warehouseConsole;
    private readonly ProductTypesConsole _productTypesConsole;
    private readonly ProductsConsole _productsConsole;

    public Program(
        WarehouseConsole warehouseConsole, 
        ProductTypesConsole productTypesConsole,
        ProductsConsole productsConsole)
    {
        _warehouseConsole = warehouseConsole;
        _productTypesConsole = productTypesConsole;
        _productsConsole = productsConsole;
    }
    
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