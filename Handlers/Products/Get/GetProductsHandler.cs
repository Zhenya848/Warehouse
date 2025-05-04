using Entities.Warehouses;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Products.Get;

public class GetProductsHandler : IHandler<IEnumerable<Product>>
{
    private readonly IRepository _repository;

    public GetProductsHandler(IRepository repository)
    {
        _repository = repository;
    }
    
    public IEnumerable<Product> Handle() =>
        _repository.GetProducts().OrderBy(n => n.Name);
}