using System.Net;

namespace CompetitionWebApi.Application.Responses;

public class SuccessResponse<T>
{
    public HttpStatusCode StatusCode { get; set; }
    public T Payload { get; set; }
}
