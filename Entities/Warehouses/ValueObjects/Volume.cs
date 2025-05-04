using CSharpFunctionalExtensions;

namespace Entities.Warehouses.ValueObjects;

public record Volume
{
    public float Wight { get; init; }
    public float Height { get; init; }
    public float Length { get; init; }
    
    public float VolumeValue => Wight * Height * Length;

    private Volume(float wight, float height, float length)
    {
        Wight = wight;
        Height = height;
        Length = length;
    }

    public static Result<Volume, Error> Create(float wight, float height, float length)
    {
        if (wight <= 0)
            return new Error("Ширина некорректна", "Ширина должна быть больше 0");
        
        if (length <= 0)
            return new Error("Длина некорректна", "Длина должна быть больше 0");
        
        if (height <= 0)
            return new Error("Высота некорректна", "Высота должна быть больше 0");
        
        var volume = new Volume(wight, height, length);

        return volume;
    }
}