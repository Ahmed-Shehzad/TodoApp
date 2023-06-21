using System.Net;

namespace Foundation.API.Types
{
    public class Error
    {
        public Error()
        {
            
        }
        public Error(HttpStatusCode reason, int code, string description)
        {
            Reason = reason;
            Code = code;
            Description = description;
        }

        public int Code { get; set; }
        public HttpStatusCode Reason { get; set; }
        public string Description { get; set; }
    }
}