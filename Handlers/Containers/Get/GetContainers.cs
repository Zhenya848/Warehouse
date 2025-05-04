using CSharpFunctionalExtensions;
using Entities;
using Entities.Warehouses;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Containers.Get;

public class GetContainers : ICommandHandler<Guid, Result<IEnumerable<Container>, Error>>
{
    private readonly IRepository _repository;

    public GetContainers(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<IEnumerable<Container>, Error> Handle(Guid warehouseId)
    {
        var warehouseExist = _repository.GetWarehouseById(warehouseId);
        
        if (warehouseExist.IsFailure)
            return warehouseExist.Error;

        return warehouseExist.Value.Containers.OrderBy(n => n.Product.Name).ToList();
    }
}