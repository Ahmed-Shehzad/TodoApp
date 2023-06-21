using Foundation.Core.Interfaces;
using Foundation.Core.Types;
using TodoApp.Data.Repositories;
using TodoApp.Domain.Dtos;
using TodoApp.Domain.Queries.Todo;

namespace TodoApp.Service.Handlers.Queries.Todo
{
    public class GetTodosQueryHandler : IQueryHandler<GetTodosQuery, TodosDto>
    {
        private readonly ITodoRepository _todoRepository;
        private readonly IMap<Domain.Entities.Todo, TodoDto> _map;

        public GetTodosQueryHandler(ITodoRepository todoRepository, IMap<Domain.Entities.Todo, TodoDto> map)
        {
            _todoRepository = todoRepository;
            _map = map;
        }

        public async Task<TodosDto> Handle(GetTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todoRepository.GetListAsync(cancellationToken: cancellationToken);
            if (todos == null)
            {
                throw new ObjectNotFoundException("No Todo Found", "Todo", request);
            }

            var todosDto = _map.Map(todos);
            return new TodosDto(todosDto);
        }
    }
}