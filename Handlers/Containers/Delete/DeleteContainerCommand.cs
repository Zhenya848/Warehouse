namespace Handlers.Containers.Delete;

public record DeleteContainerCommand(Guid WarehouseId, Guid ContainerId);