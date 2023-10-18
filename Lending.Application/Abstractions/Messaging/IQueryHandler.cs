using MediatR;

namespace Lending.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> 
    where TQuery : IRequest<TResponse>
    where TResponse : class
{
}