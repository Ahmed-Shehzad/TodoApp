using Foundation.Core.Types;
using TodoApp.Domain.Dtos;
using TodoApp.Domain.Entities;

namespace TodoApp.Service.Maps.Todo
{
    public class TodoDtoMap : MapBase<TodoDto, Domain.Entities.Todo>
    {
        public TodoDtoMap() : base(cfg =>
        {
            cfg.CreateMap<TodoDto, Domain.Entities.Todo>();
            cfg.CreateMap<TimeFrameDto, Domain.Entities.TimeFrame>();
        })
        {

        }
    }
}
