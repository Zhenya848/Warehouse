using Handlers.Containers.Create;

namespace WarehouseConsole.Warehouses;

public class ContainerConsole
{
    private readonly WarehouseConsole _warehouseConsole;
    private readonly ProductsConsole _productsConsole;
    private readonly CreateContainerHandler _createContainerHandler;

    public ContainerConsole(
        WarehouseConsole warehouseConsole,
        ProductsConsole productsConsole,
        CreateContainerHandler createContainerHandler)
    {
        _warehouseConsole = warehouseConsole;
        _productsConsole = productsConsole;
        _createContainerHandler = createContainerHandler;
    }
    
    public void ContainerCreating()
    {
        Console.WriteLine("\nСоздать контейнер");
        
        var warehouses = _warehouseConsole.ShowAllWarehouses(false);

        var indexOfWarehouse = Extensions
            .GetIntFromReadLine("\nВыберите номер склада, к которому хотите добавить контейнер: ") - 1;
        
        if (indexOfWarehouse < 0 || indexOfWarehouse >= warehouses.Count)
            return;
        
        var warehouse = warehouses[indexOfWarehouse];

        var products = _productsConsole.ShowAllProducts();
        
        var indexOfProduct = Extensions
            .GetIntFromReadLine("\nВыберите номер товара, который будет находиться в контейнере: ") - 1;
        
        if (indexOfProduct < 0 || indexOfProduct >= products.Count)
            return;
        
        var product = products[indexOfProduct];

        var count = Extensions
            .GetLongFromReadLine("\nНапишите, сколько единиц товара будет содержаться в этом контейнере: ");
        
        var command = new CreateContainerCommand(warehouse.Id, product.Id, count);
        
        var result = _createContainerHandler.Handle(command);

        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else
            Console.WriteLine("\nКонтейнер успешно создался!");
        
        Console.ReadKey();
    }
}