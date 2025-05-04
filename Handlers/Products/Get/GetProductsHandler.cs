using System.Linq.Expressions;
using Entities.Warehouses;
using Handlers.Abstractions;
using Handlers.Repositories;

namespace Handlers.Products.Get;

public class GetProductsHandler : ICommandHandler<string, IEnumerable<Product>>
{
    private readonly IRepository _repository;

    public GetProductsHandler(IRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Product> Handle(string? orderField = null)
    {
        var products = _repository.GetProducts().AsQueryable();
        
        Expression<Func<Product, object>> selector = orderField?.ToLower() switch
        {
            "имя" => product => product.Name,
            "тип" => product => product.ProductType.Name,
            "размер" => product => product.Volume.VolumeValue,
            _ => product => product.Name
        };
        
        return products.OrderBy(selector);
    }
}