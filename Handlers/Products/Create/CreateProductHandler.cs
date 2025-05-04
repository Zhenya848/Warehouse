using CSharpFunctionalExtensions;
using Entities;
using Entities.Warehouses;
using Entities.Warehouses.ValueObjects;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Products.Create;

public class CreateProductHandler : ICommandHandler<CreateProductCommand, Result<Guid, Error>>
{
    private readonly IRepository _repository;

    public CreateProductHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(CreateProductCommand command)
    {
        var productExist = _repository.GetProductByName(command.Name);

        if (productExist.IsSuccess)
            return new Error("Товар уже существует", "Товар с таким именем уже существует");
        
        var volume = Volume.Create(command.Wight, command.Height, command.Length);

        if (volume.IsFailure)
            return volume.Error;
        
        var typeExist = _repository
            .GetProductTypeById(command.TypeId);
        
        if (typeExist.IsFailure)
            return typeExist.Error;
        
        var productResult = Product.Create(typeExist.Value, command.Name, volume.Value);
        
        if (productResult.IsFailure)
            return productResult.Error;
        
        var result = _repository.CreateProduct(productResult.Value);
        
        return result;
    }
}