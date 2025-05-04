using CSharpFunctionalExtensions;
using Entities;

namespace Handlers.Abstractions;

public interface ICommandHandler<TCommand, TResult>
{
    public TResult Handle(TCommand command);
}