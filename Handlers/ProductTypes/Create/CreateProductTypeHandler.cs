using CSharpFunctionalExtensions;
using Entities;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.ProductTypes.Create;

public class CreateProductTypeHandler : ICommandHandler<string, Result<Guid, Error>>
{
    private readonly IRepository _repository;

    public CreateProductTypeHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(string name)
    {
        var productTypeResult = ProductType.Create(name);
        
        if (productTypeResult.IsFailure)
            return productTypeResult.Error;
        
        var productType = productTypeResult.Value;

        var productTypeExist = _repository
            .GetProductTypeByName(name);

        if (productTypeExist.IsSuccess)
            return new Error("Уже существует", "Тип продукта с таким именем уже существует");

        var result = _repository.CreateProductType(productType);

        return result;
    }
}