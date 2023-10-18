using MediatR;

namespace Lending.Application.Abstractions.Messaging;

public interface IQuery<T> : IRequest<T> where T : class
{
}
