using CompetitionWebApi.Domain.Enums;

namespace CompetitionWebApi.Application.Requests;

public class PerformanceRequest
{
    public string PieceName { get; set; }
    public string Composer { get; set; }
    public Period Period { get; set; }
}
