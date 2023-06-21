using Foundation.Core.Types;

namespace TodoApp.Domain.Dtos
{
    public class TodosDto : BaseCollection<TodoDto>
    {
        public TodosDto() : base()
        {
        }
        public TodosDto(IEnumerable<TodoDto> entities) : base(entities)
        {
        }
    }
}