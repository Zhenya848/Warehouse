using CSharpFunctionalExtensions;
using Entities;
using Entities.Clients;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Warehouses.Get;

public class GetWarehousesHandler : IHandler<IEnumerable<Warehouse>>
{
    private readonly IRepository _repository;

    public GetWarehousesHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<Warehouse> Handle() =>
        _repository.GetWarehouses().OrderBy(n => n.Name);
}