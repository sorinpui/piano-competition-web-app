using System.Text.Json.Serialization;

namespace CompetitionWebApi.Application.Responses;

public class ValidationErrorResponse : ErrorResponse
{
    [JsonPropertyOrder(2)]
    public Dictionary<string, string> Errors { get; set; }
}
