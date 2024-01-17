using System.Net;

namespace CompetitionApi.Application.Exceptions
{
    public class BadSectionOrderException : ServiceException
    {
        public BadSectionOrderException(HttpStatusCode statusCode) : base() 
        {
            StatusCode = statusCode;
        }

        public BadSectionOrderException(HttpStatusCode statusCode, string message) : base(message) 
        {
            StatusCode = statusCode;
        }
        public BadSectionOrderException(HttpStatusCode statusCode, string message, Exception inner) : base(message, inner) 
        {
            StatusCode = statusCode;
        }
    }
}
