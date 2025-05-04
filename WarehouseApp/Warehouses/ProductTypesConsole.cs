using Handlers.ProductTypes.Create;
using Handlers.ProductTypes.Delete;
using Handlers.ProductTypes.Get;

namespace WarehouseApp.Warehouses;

public class ProductTypesConsole
{
    private readonly CreateProductTypeHandler _createProductTypeHandler;
    private readonly GetProductTypesHandler _getProductTypesHandler;
    private readonly DeleteProductTypeHandler _deleteProductTypeHandler;

    public ProductTypesConsole(
        CreateProductTypeHandler createProductTypeHandler,
        GetProductTypesHandler getProductTypesHandler,
        DeleteProductTypeHandler deleteProductTypeHandler)
    {
        _createProductTypeHandler = createProductTypeHandler;
        _getProductTypesHandler = getProductTypesHandler;
        _deleteProductTypeHandler = deleteProductTypeHandler;
    }
    
    public void ProductTypeCreating()
    {
        Console.WriteLine("\nСоздать тип товара");
        
        Console.Write("\nНазвание: ");
        var name = Console.ReadLine();

        var result = _createProductTypeHandler.Handle(name);

        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else
            Console.WriteLine("\nТип товара успешно создан!");
        
        Console.ReadKey();
    }

    public void ProductTypeDelete()
    {
        Console.WriteLine("\nУдалить тип товара\n");
        
        var productTypes = _getProductTypesHandler.Handle().ToList();
        
        for (int i = 0; i < productTypes.Count; i++)
            Console.WriteLine($"{i + 1}. {productTypes[i].Name}");

        var index = Extensions
            .GetIntFromReadLine("\nВыберите номер типа товара, который хотите удалить: ") - 1;
        
        if (index < 0 || index >= productTypes.Count)
            return;
        
        var result = _deleteProductTypeHandler.Handle(productTypes[index].Id);
        
        if (result.IsFailure)
            Console.WriteLine(result.Error.ToResponse());
        else
            Console.WriteLine("\nТип товара успешно удален!");
        
        Console.ReadKey();
    }
}