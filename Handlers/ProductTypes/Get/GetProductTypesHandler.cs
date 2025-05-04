using Entities;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.ProductTypes.Get;

public class GetProductTypesHandler : IHandler<IEnumerable<ProductType>>
{
    private readonly IRepository _repository;

    public GetProductTypesHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<ProductType> Handle() =>
        _repository.GetProductTypes().OrderBy(n => n.Name);
}