﻿using Entities.Warehouses;
using Handlers.Products.Create;
using Handlers.Products.Delete;
using Handlers.Products.Get;
using Handlers.ProductTypes.Get;

namespace WarehouseApp.Warehouses;

public class ProductsConsole
{
    private readonly GetProductTypesHandler _getProductTypesHandler;
    private readonly CreateProductHandler _createProductHandler;
    private readonly GetProductsHandler _getProductsHandler;
    private readonly DeleteProductHandler _deleteProductHandler;

    public ProductsConsole(
        CreateProductHandler createProductHandler,
        GetProductTypesHandler getProductTypesHandler,
        GetProductsHandler getProductsHandler,
        DeleteProductHandler deleteProductHandler)
    {
        _createProductHandler = createProductHandler;
        _getProductTypesHandler = getProductTypesHandler;
        _getProductsHandler = getProductsHandler;
        _deleteProductHandler = deleteProductHandler;
    }
    
    public void ProductCreating()
    {
        Console.WriteLine("\nСоздать товар\n");
        
        var productTypes = _getProductTypesHandler.Handle().ToList();
        
        for (int i = 0; i < productTypes.Count; i++)
            Console.WriteLine($"{i + 1}. {productTypes[i].Name}");

        string waringMessage = productTypes.Count == 0
            ? "(похоже, вы пока не добавили ни одного типа товара. " +
              "Напишите 0 для выхода): "
            : "";

        int index = Extensions
            .GetIntFromReadLine("\nВыберите тип товара из существующих: " + waringMessage) - 1;

        if (index < 0 || index >= productTypes.Count)
            return;
        
        var productType = productTypes[index];
        
        Console.Write("\nНазвание товара: ");
        var name = Console.ReadLine();
        
        Console.WriteLine("\nРазмеры товара\n");

        var length = Extensions.GetFloatFromReadLine("Длина: ");
        var wight = Extensions.GetFloatFromReadLine("Ширина: ");
        var height = Extensions.GetFloatFromReadLine("Высота: ");
        
        var command = new CreateProductCommand(name, productType.Id, wight, height, length);
        
        var result = _createProductHandler.Handle(command);

        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else
            Console.WriteLine("\nТовар успешно создан!");
        
        Console.ReadKey();
    }
    
    public List<Product> ShowAllProducts()
    {
        int maxDistance = 40;
        
        string Multiply(string prompt, int count) =>
            string.Join("", Enumerable.Repeat(prompt, count));

        string GetFieldWithSpaces(string field)
        {
            string result = field;
            
            return result + Multiply(" ", maxDistance - field.Length);
        }
        
        Console.WriteLine("Показать все товары\n");

        string orderField = "";
        Console.Write("Сортировать товары? 1. Да, 2. Нет: ");
        
        var input = Console.ReadLine();

        if (input == "1")
        {
            var fields = new string[] { "Имя", "Тип", "Размер" };
            
            Console.WriteLine("\nВозможные поля для сортировки: ");
            
            for (int i = 0; i < fields.Length; i++)
                Console.WriteLine($"{i + 1}. {fields[i]}");

            var choice = Extensions.GetIntFromReadLine("\nВаш выбор: ") - 1;
            
            if (choice >= 0 && choice < fields.Length)
                orderField = fields[choice];
        }

        Console.WriteLine();
        var products = _getProductsHandler.Handle(orderField).ToList();

        string name = "";
        string type = "";
        string volume = "";
        string length = "";
        string weight = "";
        string height = "";
        string totalVolume = "";
        
        for (int i = 0; i < products.Count; i++)
        {
            var warehouse = products[i];

            name += GetFieldWithSpaces($"{i + 1}. Название: {warehouse.Name}");
            type += GetFieldWithSpaces($"Тип: {warehouse.ProductType.Name}");
            volume += GetFieldWithSpaces("Объем:");
            length += GetFieldWithSpaces($"Длина: {warehouse.Volume.Length} м");
            weight += GetFieldWithSpaces($"Ширина: {warehouse.Volume.Wight} м");
            height += GetFieldWithSpaces($"Высота: {warehouse.Volume.Height} м");
            totalVolume += GetFieldWithSpaces($"Размер: {warehouse.Volume.VolumeValue} м^3");
        }
        
        Console.WriteLine(name);
        Console.WriteLine(type);
        Console.WriteLine(volume);
        Console.WriteLine(length);
        Console.WriteLine(weight);
        Console.WriteLine(height);
        Console.WriteLine(totalVolume);
        
        Console.ReadKey();

        return products;
    }

    public void DeleteProduct()
    {
        Console.WriteLine("\nУдалить продукт");
        
        var products = ShowAllProducts();
        var index = Extensions.GetIntFromReadLine("\nВыберите номер товара, который хотите удалить: ") - 1;
        
        if (index < 0 || index >= products.Count)
            return;
        
        var result = _deleteProductHandler.Handle(products[index].Id);
        
        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else
            Console.WriteLine("\nПродукт успешно удален!");
        
        Console.ReadKey();
    }
}