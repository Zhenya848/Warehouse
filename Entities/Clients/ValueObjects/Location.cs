using CSharpFunctionalExtensions;

namespace Entities.Clients.ValueObjects;

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
        
        if (string.IsNullOrWhiteSpace(city))
            return ErrorTypes.IsRequired("город");
        
        if (string.IsNullOrWhiteSpace(address))
            return ErrorTypes.IsRequired("адресс");
        
        var location = new Location(country, city, address);

        return location;
    }
}