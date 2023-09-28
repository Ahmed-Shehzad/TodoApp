using System.Text.Json.Serialization;
using Foundation.Core.Types;
using TodoApp.Domain.Dtos;

namespace TodoApp.Domain.Commands.Todo
{
    public class DeleteTodoCommand : CommandBase<TodoDto>
    {
        public DeleteTodoCommand()
        {
            
        }
        [JsonConstructor]
        public DeleteTodoCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}