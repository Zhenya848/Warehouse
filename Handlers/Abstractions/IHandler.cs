using CSharpFunctionalExtensions;
using Entities;

namespace Handlers.Abstractions;

public interface IHandler<TResult>
{
    public TResult Handle();
}