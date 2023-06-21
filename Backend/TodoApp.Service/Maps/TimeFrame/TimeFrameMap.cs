using Foundation.Core.Types;
using TodoApp.Domain.Dtos;

namespace TodoApp.Service.Maps.TimeFrame
{
    public class TimeFrameMap : MapBase<Domain.Entities.TimeFrame, TimeFrameDto>
    {
        public TimeFrameMap() : base(cfg =>
        {
            cfg.CreateMap<Domain.Entities.TimeFrame, TimeFrameDto>();
        })
        {
        
        }
    }
}