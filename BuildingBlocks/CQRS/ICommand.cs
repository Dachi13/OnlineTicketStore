using BuildingBlocks.Library;
using MediatR;

namespace BuildingBlocks.CQRS;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}