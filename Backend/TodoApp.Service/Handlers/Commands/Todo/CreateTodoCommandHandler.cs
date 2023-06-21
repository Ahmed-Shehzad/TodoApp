using Foundation.Core.Interfaces;
using Foundation.Data.Interfaces;
using TodoApp.Data.Repositories;
using TodoApp.Domain.Commands.Todo;
using TodoApp.Domain.Dtos;
using TodoApp.Domain.Entities;

namespace TodoApp.Service.Handlers.Commands.Todo
{
    public class CreateTodoCommandHandler : ICommandHandler<CreateTodoCommand, TodoDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMap<Domain.Entities.Todo, TodoDto> _mapTodo;
        private readonly IMap<TimeFrameDto, TimeFrame> _mapTimeFrame;

        public CreateTodoCommandHandler(ITodoRepository todoRepository, IUnitOfWork unitOfWork, IMap<Domain.Entities.Todo, TodoDto> mapTodo, IMap<TimeFrameDto, TimeFrame> mapTimeFrame)
        {
            _todoRepository = todoRepository;
            _unitOfWork = unitOfWork;
            _mapTodo = mapTodo;
            _mapTimeFrame = mapTimeFrame;
        }

        public async Task<TodoDto> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var deadline = _mapTimeFrame.Map(request.Deadline);
            var todo = new Domain.Entities.Todo
            {
                Title = request.Title,
                Deadline = deadline,
                Description = request.Description
            };
            
            _todoRepository.Add(todo);
            await _unitOfWork.CommitAsync(cancellationToken);
            var todoDto = _mapTodo.Map(todo);
            return todoDto;
        }
    }
}