using CompetitionWebApi.Domain.Enums;

namespace CompetitionWebApi.Application.Dtos;

public class PerformanceDto
{
    public int PerformanceId { get; set; }
    public string ContestantName { get; set; }
    public string Piece { get; set; }
    public string Composer { get; set; }
    public string Period { get; set; }
}
