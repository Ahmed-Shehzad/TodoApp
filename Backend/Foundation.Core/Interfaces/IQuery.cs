using MediatR;

namespace Foundation.Core.Interfaces
{
    public interface IQuery {}
    
    public interface IQuery<out T>: IRequest<T>, IQuery { }
}