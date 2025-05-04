using CSharpFunctionalExtensions;
using Entities.Warehouses.ValueObjects;

namespace Entities.Warehouses;

public class Warehouse
{
    public Guid Id { get; init; }
    
    public string Name { get; private set; }
    public Volume Volume { get; private set; }
    public Location Location { get; private set; }

    public List<Container> Containers { get; private set; } = [];

    public float FreeVolume => 
        Volume.VolumeValue - Containers.Select(v => v.Product.Volume.VolumeValue * v.Quantity).Sum();

    private Warehouse(
        Guid id, 
        string name, 
        Volume volume, 
        Location location)
    {
        Id = id;
        
        Name = name;
        Volume = volume;
        Location = location;
    }

    public static Result<Warehouse, Error> Create(
        string name, 
        Volume volume, 
        Location location)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ErrorTypes.IsRequired("имя");
        
        if (name.Length > 30)
            return new Error("Некорректное имя", "Длина имени должна быть не более 30 знаков");
        
        var warehouse = new Warehouse(Guid.NewGuid(), name, volume, location);

        return warehouse;
    }

    public UnitResult<Error> Update(string name, Volume volume, Location location)
    {
        if (string.IsNullOrWhiteSpace(name))
            return ErrorTypes.IsRequired("имя");
        
        if (name.Length > 30)
            return new Error("Некорректное имя", "Длина имени должна быть не более 30 знаков");

        var oldVolume = Volume;
        Volume = volume;

        if (FreeVolume < 0)
        {
            Volume = oldVolume;

            return new Error("Ошибка при обновлении склада", 
                $"Заданный объем ({volume.VolumeValue} м^3) слишком мал для хранения уже существующих контейнеров! " +
                        $"нужный объем для хранения: {Volume.VolumeValue - FreeVolume} м^3");
        }
        
        Name = name;
        Location = location;

        return Result.Success<Error>();
    }

    public UnitResult<Error> AddContainer(Container container)
    {
        Containers.Add(container);

        if (FreeVolume < 0)
        {
            var error = new Error("Недостаточно места", "Место в складу закончилось! " +
                $"Превышение объема: {-FreeVolume} м^3");
            
            Containers.Remove(container);

            return error;
        }
        
        return Result.Success<Error>();
    }
    
    public void RemoveContainer(Guid containerId) =>
        Containers.RemoveAll(i => i.Id == containerId);
}