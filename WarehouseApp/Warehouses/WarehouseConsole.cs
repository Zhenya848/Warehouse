using Entities.Warehouses;
using Handlers.Containers.Delete;
using Handlers.Warehouses.Create;
using Handlers.Warehouses.Delete;
using Handlers.Warehouses.Get;
using Handlers.Warehouses.Update;

namespace WarehouseApp.Warehouses;

public class WarehouseConsole
{
    private readonly CreateWarehouseHandler _createWarehouseHandler;
    private readonly GetWarehousesHandler _getWarehousesHandler;
    private readonly UpdateWarehouseHandler _updateWarehouseHandler;
    private readonly DeleteWarehouseHandler _deleteWarehouseHandler;

    public WarehouseConsole(
        CreateWarehouseHandler createWarehouseHandler,
        GetWarehousesHandler getWarehousesHandler,
        UpdateWarehouseHandler updateWarehouseHandler,
        DeleteWarehouseHandler deleteWarehouseHandler)
    {
        _createWarehouseHandler = createWarehouseHandler;
        _getWarehousesHandler = getWarehousesHandler;
        _updateWarehouseHandler = updateWarehouseHandler;
        _deleteWarehouseHandler = deleteWarehouseHandler;
    }
    
    public void WarehouseCreating()
    {
        Console.WriteLine("\nСоздать склад");
        
        Console.Write("\nНазвание: ");
        var name = Console.ReadLine();
        
        Console.WriteLine("\nОбъём склада\n");

        var length = Extensions.GetFloatFromReadLine("Длина: ");
        var wight = Extensions.GetFloatFromReadLine("Ширина: ");
        var height = Extensions.GetFloatFromReadLine("Высота: ");
        
        Console.WriteLine("\nГеолокация склада\n");
        
        Console.Write("Страна: ");
        var country = Console.ReadLine();

        Console.Write("Город: ");
        var city = Console.ReadLine();

        Console.Write("Адрес: ");
        var address = Console.ReadLine();
        
        var command = new CreateWarehouseCommand(name, wight, height, length, country, city, address);
        
        var result = _createWarehouseHandler.Handle(command);

        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else 
            Console.WriteLine("\nСклад успешно создан!");
        
        Console.ReadKey();
    }
    
    public void ShowAllContainers(Warehouse warehouse)
    {
        for (int j = 0; j < warehouse.Containers.Count; j++)
        {
            var container = warehouse.Containers[j];

            Console.WriteLine($"\n{j + 1}. Контейнер:\nКоличество товара: {container.Quantity}, " +
                              $"тип товара: {container.Product.ProductType.Name}, название: {container.Product.Name}");
        }
    }
    
    public List<Warehouse> ShowAllWarehouses(bool withContainers = true)
    {
        int maxDistance = 40;
        
        string Multiply(string prompt, int count) =>
            string.Join("", Enumerable.Repeat(prompt, count));

        string GetFieldWithSpaces(string field)
        {
            string result = field;
            
            return result + Multiply(" ", maxDistance - field.Length);
        }
        
        Console.WriteLine("\nПоказать все склады\n");
        
        var warehouses = _getWarehousesHandler.Handle().ToList();

        string name = "";
        string volume = "";
        string length = "";
        string weight = "";
        string height = "";

        string position = "";
        string country = "";
        string city = "";
        string address = "";
        
        string containersCount = "";
        string volumeFree = "";
        
        for (int i = 0; i < warehouses.Count; i++)
        {
            var warehouse = warehouses[i];

            name += GetFieldWithSpaces($"{i + 1}. Склад {warehouse.Name}");
            volume += GetFieldWithSpaces("Объем:");
            length += GetFieldWithSpaces($"Длина: {warehouse.Volume.Length}");
            weight += GetFieldWithSpaces($"Ширина: {warehouse.Volume.Wight}");
            height += GetFieldWithSpaces($"Высота: {warehouse.Volume.Height}");
            position += GetFieldWithSpaces("Геолокация:");
            country += GetFieldWithSpaces($"Страна: {warehouse.Location.Country}");
            city += GetFieldWithSpaces($"Город: {warehouse.Location.City}");
            address += GetFieldWithSpaces($"Адрес: {warehouse.Location.Address}");
            containersCount += GetFieldWithSpaces($"Контейнеров: {warehouse.Containers.Count}");
            volumeFree += GetFieldWithSpaces($"Свободно места на складе: {warehouse.FreeVolume} м^3");
        }
        
        Console.WriteLine(name);
        Console.WriteLine(volume);
        Console.WriteLine(length);
        Console.WriteLine(weight);
        Console.WriteLine(height);
        
        Console.WriteLine(position);
        Console.WriteLine(country);
        Console.WriteLine(city);
        Console.WriteLine(address);
        
        Console.WriteLine(containersCount);
        Console.WriteLine(volumeFree);

        if (withContainers == false)
        {
            Console.ReadKey();
            return warehouses;
        }

        var index = Extensions
            .GetIntFromReadLine("\nНапишите сюда номер склада, чтобы узнать, что в нём хранится " +
                                "(или 0, если хотите выйти): ") - 1;

        if (index < 0 || index >= warehouses.Count)
            return warehouses;
        
        ShowAllContainers(warehouses[index]);
        
        Console.ReadKey();

        return ShowAllWarehouses();
    }

    public void WarehouseUpdate()
    {
        Console.WriteLine("\nОбновить склад");
        var warehouses = ShowAllWarehouses(false);

        var index = Extensions.GetIntFromReadLine("\nВыберите номер склада, который хотите обновить: ") - 1;
        
        if (index < 0 || index >= warehouses.Count)
            return;
        
        var warehouse = warehouses[index];
        
        Console.Write("\nНовое название: ");
        var name = Console.ReadLine();
        
        Console.WriteLine("\nИзменение объема склада\n");

        var length = Extensions.GetFloatFromReadLine("Длина: ");
        var wight = Extensions.GetFloatFromReadLine("Ширина: ");
        var height = Extensions.GetFloatFromReadLine("Высота: ");
        
        Console.WriteLine("\nИзменение геолокации склада\n");
        
        Console.Write("Страна: ");
        var country = Console.ReadLine();

        Console.Write("Город: ");
        var city = Console.ReadLine();

        Console.Write("Адрес: ");
        var address = Console.ReadLine();

        var command = new UpdateWarehouseCommand(warehouse.Id, name, wight, height, length, country, city, address);
        
        var result = _updateWarehouseHandler.Handle(command);

        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else
            Console.WriteLine("\nСклад успешно обновлен!");

        Console.ReadKey();
    }

    public void WarehouseDelete()
    {
        Console.WriteLine("\nУничтожить склад");
        var warehouses = ShowAllWarehouses(false);

        var index = Extensions.GetIntFromReadLine("\nВыберите номер склада, который " +
                                                  "хотите снести с лица земли орешником: ") - 1;
        
        if (index < 0 || index >= warehouses.Count)
            return;
        
        var warehouse = warehouses[index];
        var result = _deleteWarehouseHandler.Handle(warehouse.Id);
        
        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else
            Console.WriteLine("\nСклад успешно уничтожен!");
        
        Console.ReadKey();
    }
}