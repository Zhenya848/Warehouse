using CSharpFunctionalExtensions;
using Entities;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Products.Delete;

public class DeleteProductHandler : ICommandHandler<Guid, Guid>
{
    private readonly IRepository _repository;

    public DeleteProductHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(Guid productId)
    {
        var containers = _repository
            .GetWarehouses()
            .SelectMany(c => c.Containers);

        if (containers.Any(pi => pi.Product.Id == productId))
            return new Error("Ошибка при удалении товара",
                "Товар нельзя удалить, так как он содержится в одном из контейнеров");

        var result = _repository.DeleteProduct(productId);
        
        return result;
    }
}