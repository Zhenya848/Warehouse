using CSharpFunctionalExtensions;

namespace Entities.Warehouses.ValueObjects;

public record Location
{
    public string Country { get; init; }
    public string City { get; init; }
    public string Address { get; init; }

    private Location(string country, string city, string address)
    {
        Country = country;
        City = city;
        Address = address;
    }

    public static Result<Location, Error> Create(string country, string city, string address)
    {
        if (string.IsNullOrWhiteSpace(country))
            return ErrorTypes.IsRequired("страна");
        
        if (country.Length > 30)
            return new Error("Некорректное название страны", 
                "Длина названия страны должна быть не более 30 знаков");
        
        if (string.IsNullOrWhiteSpace(city))
            return ErrorTypes.IsRequired("город");
        
        if (city.Length > 30)
            return new Error("Некорректное название города", 
                "Длина названия города должна быть не более 30 знаков");
        
        if (string.IsNullOrWhiteSpace(address))
            return ErrorTypes.IsRequired("адрес");
        
        if (address.Length > 30)
            return new Error("Некорректное название адреса", 
                "Длина названия адреса должна быть не более 30 знаков");
        
        var location = new Location(country, city, address);

        return location;
    }
}