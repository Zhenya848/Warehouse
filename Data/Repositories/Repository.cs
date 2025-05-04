using CSharpFunctionalExtensions;
using Entities;
using Entities.Clients;
using Handlers.Repositories;

namespace Data.Repositories;

public class Repository : IRepository
{
    private readonly Database _database;

    public Repository(Database database)
    {
        _database = database;
    }

    public Guid AddWarehouse(Warehouse warehouse)
    {
        _database.Warehouses.Add(warehouse);
        
        return warehouse.Id;
    }

    public Result<Warehouse, Error> GetWarehouseById(Guid id)
    {
        var warehouseExist = _database.Warehouses
            .FirstOrDefault(i => i.Id == id);

        if (warehouseExist is null)
            return ErrorTypes.NotFound(id);
        
        return warehouseExist;
    }

    public Result<Warehouse, Error> GetWarehouseByAddress(string address)
    {
        var warehouseExist = _database.Warehouses
            .FirstOrDefault(a => a.Location.Address == address);

        if (warehouseExist == null)
            return ErrorTypes.NotFound();
        
        return warehouseExist;
    }

    public IEnumerable<Warehouse> GetWarehouses() =>
        _database.Warehouses;

    public Guid DeleteWarehouse(Guid id)
    {
        _database.Warehouses.RemoveAll(i => i.Id == id);
        
        return id;
    }

    public Guid UpdateWarehouse(Warehouse warehouse)
    {
        var index = _database.Warehouses.IndexOf(warehouse);
        _database.Warehouses[index] = warehouse;
        
        return warehouse.Id;
    }

    public Result<Guid, Error> CreateProduct(Product product)
    {
        _database.Products.Add(product);

        return product.Id;
    }

    public IEnumerable<Product> GetProducts() =>
        _database.Products;

    public Result<Product, Error> GetProductById(Guid id)
    {
        var productExist = _database.Products
            .FirstOrDefault(i => i.Id == id);
        
        if (productExist is null)
            return ErrorTypes.NotFound(id);
        
        return productExist;
    }

    public Result<Product, Error> GetProductByName(string name)
    {
        var productExist = _database.Products
            .FirstOrDefault(n => n.Name == name);

        if (productExist is null)
            return ErrorTypes.NotFound();
        
        return productExist;
    }

    public Guid DeleteProduct(Guid id)
    {
        _database.Products.RemoveAll(i => i.Id == id);
        
        return id;
    }

    public Result<Guid, Error> CreateProductType(ProductType productType)
    {
        _database.ProductTypes.Add(productType);
        
        return productType.Id;
    }

    public IEnumerable<ProductType> GetProductTypes() =>
        _database.ProductTypes;

    public Result<ProductType, Error> GetProductTypeById(Guid id)
    {
        var typeExist = _database.ProductTypes
            .FirstOrDefault(i => i.Id == id);
        
        if (typeExist is null)
            return ErrorTypes.NotFound(id);
        
        return typeExist;
    }

    public Result<ProductType, Error> GetProductTypeByName(string name)
    {
        var typeExist = _database.ProductTypes
            .FirstOrDefault(i => i.Name == name);
        
        if (typeExist is null)
            return ErrorTypes.NotFound();
        
        return typeExist;
    }

    public Guid DeleteProductType(Guid id)
    {
        _database.ProductTypes.RemoveAll(i => i.Id == id);
        
        return id;
    }
}