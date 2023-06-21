using Foundation.Core.Types;
using TodoApp.Domain.Dtos;
using TodoApp.Domain.Entities;

namespace TodoApp.Service.Maps.Todo
{
    public class TodoMap : MapBase<Domain.Entities.Todo, TodoDto>
    {
        public TodoMap() : base(cfg =>
        {
            cfg.CreateMap<Domain.Entities.Todo, TodoDto>();
            cfg.CreateMap<Domain.Entities.TimeFrame, TimeFrameDto>();
        })
        {

        }
    }
}