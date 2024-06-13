using Bookify.Domain;
using MediatR;

namespace Bookify.Application;

public interface IQuerHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{

}
