using Entities;
using Entities.Clients;

namespace Data;

public class Database
{
    public List<Warehouse> Warehouses { get; } = new List<Warehouse>();
    public List<Product> Products { get; } = new List<Product>();
    public List<ProductType> ProductTypes { get; } = new List<ProductType>();
}