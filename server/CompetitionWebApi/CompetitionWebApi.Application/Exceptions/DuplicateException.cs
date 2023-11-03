using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class DuplicateException : ServiceException
{
    public DuplicateException() 
    {
        StatusCode = HttpStatusCode.Conflict;
    }
}
