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
        var result = _repository.DeleteProduct(productId);
        
        return result;
    }
}