using Foundation.Core.Types;
using TodoApp.Domain.Dtos;

namespace TodoApp.Domain.Queries.Todo
{
    public class GetTodoByIdQuery : QueryBase<TodoDto>
    {
        public GetTodoByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}