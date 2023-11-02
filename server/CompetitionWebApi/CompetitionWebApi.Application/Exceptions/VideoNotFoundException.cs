using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class VideoNotFoundException : ServiceException
{
    public VideoNotFoundException()
    {
        Status = HttpStatusCode.NotFound;
    }
}
