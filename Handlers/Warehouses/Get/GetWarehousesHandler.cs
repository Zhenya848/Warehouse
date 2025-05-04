using System.Linq.Expressions;
using CSharpFunctionalExtensions;
using Entities;
using Entities.Warehouses;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Warehouses.Get;

public class GetWarehousesHandler : ICommandHandler<string, IEnumerable<Warehouse>>
{
    private readonly IRepository _repository;

    public GetWarehousesHandler(IRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Warehouse> Handle(string? orderField = null)
    {
        var warehouses = _repository.GetWarehouses().AsQueryable();
        
        Expression<Func<Warehouse, object>> selector = orderField?.ToLower() switch
        {
            "имя" => warehouse => warehouse.Name,
            "размер" => warehouse => warehouse.Volume.VolumeValue,
            "страна" => warehouse => warehouse.Location.Country,
            "город" => warehouse => warehouse.Location.City,
            "адрес" => warehouse => warehouse.Location.Address,
            _ => warehouse => warehouse.Name
        };
        
        return warehouses.OrderBy(selector);
    }
}