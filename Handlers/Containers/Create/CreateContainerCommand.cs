using Entities.Clients;

namespace Handlers.Containers.Create;

public record CreateContainerCommand(Guid WarehouseId, Guid ProductId, long Quantity);