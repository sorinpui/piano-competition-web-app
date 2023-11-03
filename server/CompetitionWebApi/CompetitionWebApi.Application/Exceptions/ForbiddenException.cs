using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class ForbiddenException : ServiceException
{
    public ForbiddenException()
    {
        StatusCode = HttpStatusCode.Forbidden;
    }
}
