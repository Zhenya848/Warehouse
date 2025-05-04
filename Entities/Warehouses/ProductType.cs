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
        
        if (name.Length > 30)
            return new Error("Некорректное имя", "Длина имени должна быть не более 30 знаков");
        
        var productType = new ProductType(Guid.NewGuid(), name);
        
        return productType;
    }
}