namespace Foundation.Core.Types
{
    public class Range<T>
    {
        protected Range()
        {
            
        }
        protected Range(T? from, T? to)
        {
            From = from;
            To = to;
        }
        
        public T? From { get; set; }
        public T? To { get; set; }
    }
}