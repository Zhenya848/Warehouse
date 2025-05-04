using CSharpFunctionalExtensions;
using Entities.Warehouses.ValueObjects;

namespace Entities.Warehouses;

public class Product
{
    public Guid Id { get; init; }
    
    public ProductType ProductType { get; init; }
    public string Name { get; init; }
    public Volume Volume { get; init; }

    private Product(Guid id, ProductType productType, string name, Volume volume)
    {
        Id = id;
        
        ProductType = productType;
        Name = name;
        Volume = volume;
    }

    public static Result<Product, Error> Create(ProductType productType, string name, Volume volume)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ErrorTypes.IsRequired("имя");
        
        var product = new Product(Guid.NewGuid(), productType, name, volume);
        
        return product;
    }
}