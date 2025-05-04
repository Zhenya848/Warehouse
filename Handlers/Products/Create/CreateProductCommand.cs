namespace Handlers.Products.Create;

public record CreateProductCommand(string Name, Guid TypeId, float Wight, float Height, float Length);