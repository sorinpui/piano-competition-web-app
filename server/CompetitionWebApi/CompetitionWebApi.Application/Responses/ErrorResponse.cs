using System.Net;
using System.Text.Json.Serialization;

namespace CompetitionWebApi.Application.Responses;

public class ErrorResponse
{
    [JsonPropertyOrder(0)]
    public string ErrorMessage { get; set; }

    [JsonPropertyOrder(1)]
    public HttpStatusCode Status { get; set; }
}
