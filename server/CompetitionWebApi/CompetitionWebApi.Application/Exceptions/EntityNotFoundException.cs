using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class EntityNotFoundException : ServiceException
{
    public EntityNotFoundException() 
    {
        StatusCode = HttpStatusCode.NotFound;
    }
}
