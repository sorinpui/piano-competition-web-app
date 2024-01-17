namespace CompetitionApi.Application.Responses
{
    public record ValidationErrorResponse(string Message, Dictionary<string, string> Errors);
}
