using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class InvalidRequestException : ServiceException
{
    public InvalidRequestException()
    {
        StatusCode = HttpStatusCode.BadRequest;
    }
}
