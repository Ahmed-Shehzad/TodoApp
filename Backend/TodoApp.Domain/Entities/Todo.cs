using Foundation.Core.Interfaces;
using Foundation.Core.Types;

namespace TodoApp.Domain.Entities
{
    public class Todo : BaseEntity, IAggregateRoot
    {
        public Todo()
        {
        }

        public string Title { get; set; }
        public string? Description { get; set; }
        public TimeFrame Deadline { get; set; }
        public bool IsDone { get; set; }
        public bool IsOverdue => !IsDone && DateTime.UtcNow > Deadline.To;
        public DateTime? UpdatedAt { get; set; }
    }
}
