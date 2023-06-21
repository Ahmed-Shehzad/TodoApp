using Foundation.Core.Interfaces;
using Foundation.Core.Types;
using Foundation.Data.Interfaces;
using TodoApp.Data.Repositories;
using TodoApp.Domain.Commands.Todo;
using TodoApp.Domain.Dtos;
using TodoApp.Domain.Entities;

namespace TodoApp.Service.Handlers.Commands.Todo
{
    public class UpdateTodoCommandHandler : ICommandHandler<UpdateTodoCommand, TodoDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMap<Domain.Entities.Todo, TodoDto> _mapTodo;
        private readonly IMap<TimeFrameDto, TimeFrame> _mapTimeFrame;

        public UpdateTodoCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork, IMap<Domain.Entities.Todo, TodoDto> map, IMap<TimeFrameDto, TimeFrame> mapTimeFrame)
        {
            _todoRepository = todoRepository;
            _unitOfWork = unitOfWork;
            _mapTodo = map;
            _mapTimeFrame = mapTimeFrame;
        }

        public async Task<TodoDto> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            if (todo == null)
            {
                throw new ObjectNotFoundException(nameof(UpdateTodoCommand), "Todo", request.Id);
            }

            todo.Description = request.Description;
            todo.Title = request.Title;
            todo.Deadline = _mapTimeFrame.Map(request.Deadline);
            todo.IsDone = request.IsDone;
            todo.UpdatedAt = DateTime.UtcNow;
        
            _todoRepository.Update(todo);
            await _unitOfWork.CommitAsync(cancellationToken);
        
            var todoDto = _mapTodo.Map(todo);
            return todoDto;
        }
    }
}