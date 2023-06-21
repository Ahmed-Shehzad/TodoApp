using Foundation.Core.Interfaces;
using Foundation.Core.Types;
using Foundation.Data.Interfaces;
using TodoApp.Data.Repositories;
using TodoApp.Domain.Commands.Todo;
using TodoApp.Domain.Dtos;

namespace TodoApp.Service.Handlers.Commands.Todo
{
    public class DeleteTodoCommandHandler : ICommandHandler<DeleteTodoCommand, TodoDto?>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMap<Domain.Entities.Todo?, TodoDto?> _map;

        public DeleteTodoCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork, IMap<Domain.Entities.Todo?, TodoDto?> map)
        {
            _todoRepository = todoRepository;
            _unitOfWork = unitOfWork;
            _map = map;
        }

        public async Task<TodoDto?> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (todo == null)
            {
                // throw new ObjectNotFoundException("Not Found", "Todo", request.Id);
                return null;
            }
        
            _todoRepository.Remove(todo);
            await _unitOfWork.CommitAsync(cancellationToken);
        
            var todoDto = _map.Map(todo);
            return todoDto;
        }
    }
}