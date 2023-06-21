using Foundation.Core.Types;

namespace TodoApp.Domain.Entities
{
    public class TimeFrame : Range<DateTime>
    {
        public TimeFrame() : base()
        {
        }
        public TimeFrame(DateTime from, DateTime to) : base(from, to)
        {
        }
    }
}