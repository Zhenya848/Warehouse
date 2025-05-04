using CSharpFunctionalExtensions;
using Entities;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.ProductTypes.Delete;

public class DeleteProductTypeHandler : ICommandHandler<Guid, Result<Guid, Error>>
{
    private readonly IRepository _repository;

    public DeleteProductTypeHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(Guid productTypeId)
    {
        var products = _repository.GetProducts();

        if (products.Any(pti => pti.ProductType.Id == productTypeId))
            return new Error("Ошибка при удалении типа товара",
                $"Тип товара нельзя удалить, так как он используется в одном из продуктов");

        var result = _repository.DeleteProductType(productTypeId);

        return result;
    }
}