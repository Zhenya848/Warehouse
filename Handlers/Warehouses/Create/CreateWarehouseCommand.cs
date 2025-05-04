namespace Handlers.Warehouses.Create;

public record CreateWarehouseCommand(string Name, float Wight, float Height, float Length, string Country,
    string City, string Address);