using CompetitionWebApi.Domain.Enums;

namespace CompetitionWebApi.Application.Requests;

public class PerformanceRequest
{
    public string Name { get; set; }
    public string Composer { get; set; }
    public Period Period { get; set; }
    public string VideoUri { get; set; }
    public int UserId { get; set; }
}
