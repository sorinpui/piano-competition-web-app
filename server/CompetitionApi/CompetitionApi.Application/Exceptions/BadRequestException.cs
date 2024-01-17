using System.Net;

namespace CompetitionApi.Application.Exceptions
{
    public class BadRequestException : ServiceException
    {
        public BadRequestException() : base()
        {
            StatusCode = HttpStatusCode.BadRequest;
        }

        public BadRequestException(string message) : base(message)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
        public BadRequestException(string message, Exception inner) : base(message, inner)
        {
            StatusCode = HttpStatusCode.BadRequest;
        }
    }
}
