using MediatR;

namespace Foundation.Core.Interfaces
{
    public interface ICommandHandler<in TCommand>: IRequestHandler<TCommand> where TCommand: ICommand<Unit>, IRequest
    {
        
    }
    
    public interface ICommandHandler<in TCommand, TResponse>: IRequestHandler<TCommand, TResponse> where TCommand: ICommand<TResponse>
    {
        
    }
}