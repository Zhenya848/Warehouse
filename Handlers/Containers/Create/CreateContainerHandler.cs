using System.ComponentModel;
using CSharpFunctionalExtensions;
using Entities;
using Handlers.Abstractions;
using Handlers.Repositories;
using Container = Entities.Warehouses.Container;

namespace Handlers.Containers.Create;

public class CreateContainerHandler : ICommandHandler<CreateContainerCommand, Result<Guid, Error>>
{
    private readonly IRepository _repository;

    public CreateContainerHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(CreateContainerCommand command)
    {
        var warehouseExist = _repository
            .GetWarehouseById(command.WarehouseId);

        if (warehouseExist.IsFailure)
            return warehouseExist.Error;
        
        var productExist = _repository
            .GetProductById(command.ProductId);
        
        if (productExist.IsFailure)
            return productExist.Error;
        
        var containerResult = Container.Create(command.Quantity, productExist.Value);
        
        if (containerResult.IsFailure)
            return containerResult.Error;
        
        var addContainerResult = warehouseExist.Value.AddContainer(containerResult.Value);
        
        if (addContainerResult.IsFailure)
            return addContainerResult.Error;

        var result = _repository.UpdateWarehouse(warehouseExist.Value);
        
        return result;
    }
}