using CSharpFunctionalExtensions;
using Entities;
using Entities.Clients;
using Entities.Clients.ValueObjects;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Warehouses.Create;

public class CreateWarehouseHandler : ICommandHandler<CreateWarehouseCommand, Guid>
{
    private readonly IRepository _repository;

    public CreateWarehouseHandler(IRepository repository)
    {
        _repository = repository;
    }

    public Result<Guid, Error> Handle(CreateWarehouseCommand command)
    {
        var volume = Volume.Create(command.Wight, command.Height, command.Length);

        if (volume.IsFailure)
            return volume.Error;

        var location = Location.Create(command.Country, command.City, command.Address);
        
        if (location.IsFailure)
            return location.Error;
        
        var warehouseResult = Warehouse
            .Create(command.Name, volume.Value, location.Value);
        
        if (warehouseResult.IsFailure)
            return warehouseResult.Error;
        
        var warehouse = warehouseResult.Value;
        
        var warehouseExist = _repository
            .GetWarehouseByAddress(warehouse.Location.Address);

        if (warehouseExist.IsSuccess)
            return new Error("Склад уже существует",
                $"Какой - то склад уже зарегистрирован под адресом {warehouse.Location.Address}");
        
        var result = _repository.AddWarehouse(warehouse);

        return result;
    }
}