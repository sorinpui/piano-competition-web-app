using System.Net;

namespace CompetitionApi.Application.Exceptions
{
    public abstract class ServiceException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public ServiceException() { }
        public ServiceException(string message) : base(message) { }
        public ServiceException(string message, Exception inner) : base(message, inner) { }
    }
}
