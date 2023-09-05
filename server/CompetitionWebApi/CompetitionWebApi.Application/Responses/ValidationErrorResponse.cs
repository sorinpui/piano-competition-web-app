namespace CompetitionWebApi.Application.Responses;

public class ValidationErrorResponse : ErrorResponse
{
    public Dictionary<string, string> Errors { get; set; }
}
