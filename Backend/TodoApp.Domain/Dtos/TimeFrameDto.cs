using Foundation.Core.Types;

namespace TodoApp.Domain.Dtos
{
    public class TimeFrameDto : Range<DateTime>
    {
        public TimeFrameDto() : base()
        {
        }
        
        public TimeFrameDto(DateTime from, DateTime to) : base(from, to)
        {
        }
    }
}