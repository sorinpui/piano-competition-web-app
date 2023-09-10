using System.Text.Json.Serialization;

namespace CompetitionWebApi.Application.Responses;

public class ValidationErrorResponse : ErrorResponse
{
    [JsonPropertyOrder(4)]
    public Dictionary<string, string> Errors { get; set; }
}
