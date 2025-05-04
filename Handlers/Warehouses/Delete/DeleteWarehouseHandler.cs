using CSharpFunctionalExtensions;
using Entities;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Warehouses.Delete;

public class DeleteWarehouseHandler : ICommandHandler<Guid, Result<Guid, Error>>
{
    private readonly IRepository _repository;

    public DeleteWarehouseHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(Guid warehouseId)
    {
        var result = _repository.DeleteWarehouse(warehouseId);
        
        return result;
    }
}