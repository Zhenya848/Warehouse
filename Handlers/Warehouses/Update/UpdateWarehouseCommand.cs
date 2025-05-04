namespace Handlers.Warehouses.Update;

public record UpdateWarehouseCommand(Guid Id, string Name, float Wight, float Height, float Length, string Country,
    string City, string Address);