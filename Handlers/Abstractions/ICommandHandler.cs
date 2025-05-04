using CSharpFunctionalExtensions;
using Entities;

namespace Handlers.Abstractions;

public interface ICommandHandler<TCommand, TResult>
{
    public Result<TResult, Error> Handle(TCommand command);
}