using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CompetitionWebApi.Application.Responses;

public class ErrorResponse
{
    [JsonPropertyOrder(0)]
    public string? Title { get; set; }

    [JsonPropertyOrder(1)]
    public string? Detail { get; set; }

    [JsonPropertyOrder(2)]
    public DateTime Timestamp { get; set; }

    public ErrorResponse()
    {
        Timestamp = DateTime.UtcNow;
    }
}
