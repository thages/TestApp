using MediatR;

namespace TestApp.Application.Interfaces
{
    public interface IApplicationCommand<ReturnObjectType> : IRequest<ReturnObjectType> { }
}
