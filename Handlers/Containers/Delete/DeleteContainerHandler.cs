using CSharpFunctionalExtensions;
using Entities;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Containers.Delete;

public class DeleteContainerHandler : ICommandHandler<DeleteContainerCommand, Guid>
{
    private readonly IRepository _repository;

    public DeleteContainerHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(DeleteContainerCommand command)
    {
        var warehouseExist = _repository
            .GetWarehouseById(command.WarehouseId);
        
        if (warehouseExist.IsFailure)
            return warehouseExist.Error;
        
        warehouseExist.Value.RemoveContainer(command.ContainerId);
        
        var result = _repository.UpdateWarehouse(warehouseExist.Value);

        return result;
    }
}