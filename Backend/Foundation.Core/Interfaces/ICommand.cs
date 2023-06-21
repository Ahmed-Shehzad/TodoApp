using MediatR;

namespace Foundation.Core.Interfaces
{
    public interface ICommand {}
    
    public interface ICommand<out T>: IRequest<T>, ICommand { }
}