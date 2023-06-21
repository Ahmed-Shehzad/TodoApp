using Foundation.Core.Types;
using TodoApp.Domain.Dtos;

namespace TodoApp.Service.Maps.TimeFrame
{
    public class TimeFrameDtoMap : MapBase<TimeFrameDto, Domain.Entities.TimeFrame>
    {
        public TimeFrameDtoMap() : base(cfg =>
        {
            cfg.CreateMap<TimeFrameDto, Domain.Entities.TimeFrame>();
        })
        {
        
        }
    }
}