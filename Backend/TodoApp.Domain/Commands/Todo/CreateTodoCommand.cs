using System.Text.Json.Serialization;
using Foundation.Core.Types;
using TodoApp.Domain.Dtos;

namespace TodoApp.Domain.Commands.Todo
{
    public class CreateTodoCommand : CommandBase<TodoDto>
    {
        public CreateTodoCommand()
        {
                
        }
        [JsonConstructor]
        public CreateTodoCommand(string title, TimeFrameDto deadline)
        {
            Title = title;
            Deadline = deadline;
        }
        [JsonConstructor]
        public CreateTodoCommand(string title, string? description, TimeFrameDto deadline)
        {
            Title = title;
            Description = description;
            Deadline = deadline;
        }

        public string Title { get; set; }
        public string? Description { get; set; }
        public TimeFrameDto Deadline { get; set; }
    }
}