using System.Text.Json.Serialization;
using Foundation.Core.Types;
using TodoApp.Domain.Dtos;

namespace TodoApp.Domain.Commands.Todo
{
    public class UpdateTodoCommand : CommandBase<TodoDto>
    {
        public UpdateTodoCommand()
        {
            
        }
        [JsonConstructor]
        public UpdateTodoCommand(Guid id, string title, TimeFrameDto deadline, bool isDone)
        {
            Id = id;
            Title = title;
            Deadline = deadline;
            IsDone = isDone;
        }
        [JsonConstructor]
        public UpdateTodoCommand(Guid id, string title, string? description, TimeFrameDto deadline, bool isDone)
        {
            Id = id;
            Title = title;
            Description = description;
            Deadline = deadline;
            IsDone = isDone;
        }

        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool IsDone { get; set; }
        public TimeFrameDto Deadline { get; set; }
    }
}