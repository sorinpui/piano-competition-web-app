using System.Text.Json.Serialization;

namespace CompetitionWebApi.Application.Responses;

public class ErrorResponse
{
    [JsonPropertyOrder(0)]
    public string Type { get; set; }

    [JsonPropertyOrder(1)]
    public string Title { get; set; }

    [JsonPropertyOrder(2)]
    public int Status { get; set; }

    [JsonPropertyOrder(3)]
    public string Detail { get; set; }
}
