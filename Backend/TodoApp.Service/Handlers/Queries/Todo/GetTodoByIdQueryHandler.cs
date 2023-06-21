using Foundation.Core.Interfaces;
using Foundation.Core.Types;
using TodoApp.Data.Repositories;
using TodoApp.Domain.Dtos;
using TodoApp.Domain.Queries.Todo;

namespace TodoApp.Service.Handlers.Queries.Todo
{
    public class GetTodoByIdQueryHandler : IQueryHandler<GetTodoByIdQuery, TodoDto?>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMap<Domain.Entities.Todo?, TodoDto?> _map;

        public GetTodoByIdQueryHandler(ITodoRepository todoRepository, IMap<Domain.Entities.Todo?, TodoDto?> map)
        {
            _todoRepository = todoRepository;
            _map = map;
        }

        public async Task<TodoDto?> Handle(GetTodoByIdQuery request, CancellationToken cancellationToken)
        {
            var todo = await _todoRepository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
            var todoDto = _map.Map(todo);
            return todoDto;
        }
    }
}