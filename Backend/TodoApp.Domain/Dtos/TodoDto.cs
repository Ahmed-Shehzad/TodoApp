namespace TodoApp.Domain.Dtos
{
    public class TodoDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public TimeFrameDto? Deadline { get; set; }
        public bool IsDone { get; set; }
        public bool IsOverdue { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}