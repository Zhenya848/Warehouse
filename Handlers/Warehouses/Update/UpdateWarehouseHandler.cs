using CSharpFunctionalExtensions;
using Entities;
using Entities.Warehouses.ValueObjects;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Warehouses.Update;

public class UpdateWarehouseHandler : ICommandHandler<UpdateWarehouseCommand, Result<Guid, Error>>
{
    private readonly IRepository _repository;

    public UpdateWarehouseHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(UpdateWarehouseCommand command)
    {
        var volume = Volume.Create(command.Wight, command.Height, command.Length);

        if (volume.IsFailure)
            return volume.Error;

        var location = Location.Create(command.Country, command.City, command.Address);
        
        if (location.IsFailure)
            return location.Error;
        
        var warehouseExist = _repository
            .GetWarehouseById(command.Id);

        if (warehouseExist.IsFailure)
            return warehouseExist.Error;
        
        var warehouse = warehouseExist.Value;
        var updateWarehouseResult = warehouse.Update(command.Name, volume.Value, location.Value);
        
        if (updateWarehouseResult.IsFailure)
            return updateWarehouseResult.Error;
        
        var result = _repository.UpdateWarehouse(warehouse);
        
        return result;
    }
}