using Entities.Warehouses;
using Handlers.Containers.Create;
using Handlers.Containers.Delete;
using Handlers.Warehouses.Create;

namespace WarehouseApp.Warehouses;

public class ContainerConsole
{
    private readonly WarehouseConsole _warehouseConsole;
    private readonly ProductsConsole _productsConsole;
    private readonly CreateContainerHandler _createContainerHandler;
    private readonly DeleteContainerHandler _deleteContainerHandler;

    public ContainerConsole(
        WarehouseConsole warehouseConsole,
        ProductsConsole productsConsole,
        CreateContainerHandler createContainerHandler,
        DeleteContainerHandler deleteContainerHandler)
    {
        _warehouseConsole = warehouseConsole;
        _productsConsole = productsConsole;
        _createContainerHandler = createContainerHandler;
        _deleteContainerHandler = deleteContainerHandler;
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
    
    public void WarehouseDeleteContainer()
    {
        Console.WriteLine("\nУбрать контейнер из склада");
        var warehouses = _warehouseConsole.ShowAllWarehouses(false);

        var index = Extensions.GetIntFromReadLine("\nВыберите номер склада: ") - 1;
        
        if (index < 0 || index >= warehouses.Count)
            return;
        
        var warehouse = warehouses[index];
        
        _warehouseConsole.ShowAllContainers(warehouse);

        var indexOfContainer = Extensions
            .GetIntFromReadLine("\nВыберите номер контейнера, который хотите убрать: ") - 1;
        
        if (indexOfContainer < 0 || indexOfContainer >= warehouse.Containers.Count)
            return;
        
        var command = new DeleteContainerCommand(warehouse.Id, warehouse.Containers[indexOfContainer].Id);
        
        var result = _deleteContainerHandler.Handle(command);
        
        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else
            Console.WriteLine("Контейнер успешно убран!");
        
        Console.ReadKey();
    }
    
    public void MoveContainer()
    {
        Console.WriteLine("\nПереместить контейнер с одного склада в другой");
        var warehouses = _warehouseConsole.ShowAllWarehouses(false);

        var indexOfOldWarehouse = Extensions.GetIntFromReadLine("\nВыберите номер склада: ") - 1;
        
        if (indexOfOldWarehouse < 0 || indexOfOldWarehouse >= warehouses.Count)
            return;
        
        var oldWarehouse = warehouses[indexOfOldWarehouse];
        
        _warehouseConsole.ShowAllContainers(oldWarehouse);

        var indexOfContainer = Extensions
            .GetIntFromReadLine("\nВыберите номер контейнера, который хотите переместить: ") - 1;
        
        if (indexOfContainer < 0 || indexOfContainer >= oldWarehouse.Containers.Count)
            return;
        
        var container = oldWarehouse.Containers[indexOfContainer];
        
        var indexOfNewWarehouse = Extensions
            .GetIntFromReadLine("\nВыберите номер склада, куда хотите перенести контейнер: ") - 1;
        
        if (indexOfNewWarehouse < 0 || indexOfNewWarehouse >= warehouses.Count)
            return;
        
        var newWarehouse = warehouses[indexOfNewWarehouse];

        var createContainerToNewWarehouseCommand = 
            new CreateContainerCommand(newWarehouse.Id, container.Product.Id, container.Quantity);
        
        var createContainerToNewWarehouseResult = _createContainerHandler
            .Handle(createContainerToNewWarehouseCommand);

        if (createContainerToNewWarehouseResult.IsFailure)
        {
            Console.WriteLine(createContainerToNewWarehouseResult.Error.ToResponse());
            Console.ReadKey();
            
            return;
        }
        
        var deleteContainerFromOldWarehouseCommand = 
            new DeleteContainerCommand(oldWarehouse.Id, container.Id);
        
        var deleteContainerFromOldWarehouseResult = _deleteContainerHandler
            .Handle(deleteContainerFromOldWarehouseCommand);

        if (deleteContainerFromOldWarehouseResult.IsFailure)
        {
            var deleteContainerFromNewWarehouseCommand = 
                new DeleteContainerCommand(newWarehouse.Id, container.Id);
            
            _deleteContainerHandler.Handle(deleteContainerFromNewWarehouseCommand);

            Console.WriteLine(deleteContainerFromOldWarehouseResult.Error.ToResponse());
        }
        else
            Console.WriteLine("\nКонтейнер успешно перемещен!");
        
        Console.ReadKey();
    }
}