using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class ForbiddenException : ServiceException
{
    public ForbiddenException()
    {
        Status = HttpStatusCode.Forbidden;
    }
}
