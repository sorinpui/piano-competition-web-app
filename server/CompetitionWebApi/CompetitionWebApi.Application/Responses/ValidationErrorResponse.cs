using System.Text.Json.Serialization;

namespace CompetitionWebApi.Application.Responses;

public class ValidationErrorResponse : ErrorResponse
{
    [JsonPropertyOrder(3)]
    public Dictionary<string, string>? Errors { get; set; }
}
