namespace CompetitionWebApi.Application.Requests;

public class CommentRequest
{
    public string Message { get; set; }
    public int PerformanceId { get; set; }
    public int UserId { get; set; }
}
