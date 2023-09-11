using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class ForbiddenException : Exception, IServiceException
{ 
    public HttpStatusCode Status => HttpStatusCode.Forbidden;

    public string Title { get; set; }

    public string Detail { get; set; }

    public ForbiddenException() { }
}
