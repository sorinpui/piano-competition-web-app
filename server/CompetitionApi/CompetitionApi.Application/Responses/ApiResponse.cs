namespace CompetitionApi.Application.Responses
{
    public record ApiResponse<T>(bool IsSuccess, string Message, T? Payload);
}
