using CSharpFunctionalExtensions;

namespace Entities;

public class ProductType
{
    public Guid Id { get; init; }
    public string Name { get; init; }

    private ProductType(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static Result<ProductType, Error> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ErrorTypes.IsRequired("имя");
        
        var productType = new ProductType(Guid.NewGuid(), name);
        
        return productType;
    }
}