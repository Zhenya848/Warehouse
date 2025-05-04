using CSharpFunctionalExtensions;
using Entities;
using Entities.Clients;

namespace Handlers.Repositories;

public interface IRepository
{
    public Guid AddWarehouse(Warehouse warehouse);
    public Result<Warehouse, Error> GetWarehouseById(Guid id);
    public Result<Warehouse, Error> GetWarehouseByAddress(string address);
    public IEnumerable<Warehouse> GetWarehouses();
    public Guid DeleteWarehouse(Guid id);
    public Guid UpdateWarehouse(Warehouse warehouse);

    public Result<Guid, Error> CreateProduct(Product product);
    public IEnumerable<Product> GetProducts();
    public Result<Product, Error> GetProductById(Guid id);
    public Result<Product, Error> GetProductByName(string name);
    public Guid DeleteProduct(Guid id);
    
    public Result<Guid, Error> CreateProductType(ProductType productType);
    public IEnumerable<ProductType> GetProductTypes();
    public Result<ProductType, Error> GetProductTypeById(Guid id);
    public Result<ProductType, Error> GetProductTypeByName(string name);
    public Guid DeleteProductType(Guid id);
}