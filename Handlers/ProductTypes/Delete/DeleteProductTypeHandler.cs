using CSharpFunctionalExtensions;
using Entities;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.ProductTypes.Delete;

public class DeleteProductTypeHandler : ICommandHandler<Guid, Guid>
{
    private readonly IRepository _repository;

    public DeleteProductTypeHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public Result<Guid, Error> Handle(Guid productTypeId)
    {
        var result = _repository.DeleteProductType(productTypeId);

        return result;
    }
}