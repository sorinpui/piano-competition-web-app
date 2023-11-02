using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class DuplicateException : ServiceException
{
    public DuplicateException() 
    {
        Status = HttpStatusCode.Conflict;
    }
}
