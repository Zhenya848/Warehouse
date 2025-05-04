using CSharpFunctionalExtensions;

namespace Entities.Warehouses;

public class Container
{
    public Guid Id { get; init; }
    
    public long Quantity { get; init; }
    public Product Product { get; init; }

    private Container(Guid id, long quantity, Product product)
    {
        Id = id;
        
        Quantity = quantity;
        Product = product;
    }

    public static Result<Container, Error> Create(long quantity, Product product)
    {
        if (quantity <= 0)
            return new Error("Некорректное количество", "Количество должно быть больше 0");
        
        var container = new Container(Guid.NewGuid(), quantity, product);

        return container;
    }
}