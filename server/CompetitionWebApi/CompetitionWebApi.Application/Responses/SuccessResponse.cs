using System.Text.Json.Serialization;

namespace CompetitionWebApi.Application.Responses;

public class SuccessResponse<T>
{
    [JsonPropertyOrder(0)]
    public string? Message { get; set; }

    [JsonPropertyOrder(1)]
    public T? Payload { get; set; }
}
