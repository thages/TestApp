using MediatR;

namespace TestApp.Application.Interfaces
{
    public interface IApplicationCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        new Task<TResponse> Handle(TRequest command, CancellationToken cancellationToken);
    }
}
