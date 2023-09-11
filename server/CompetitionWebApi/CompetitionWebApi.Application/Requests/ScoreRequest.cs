namespace CompetitionWebApi.Application.Requests;

public class ScoreRequest
{
    public int Interpretation { get; set; }
    public int Technicality { get; set; }
    public int Difficulty { get; set; }
    public int PerformanceId { get; set; }
}
