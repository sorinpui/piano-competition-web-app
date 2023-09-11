using CompetitionWebApi.Domain.Enums;

namespace CompetitionWebApi.Application.Dtos;

public class PerformanceDto
{
    public int PerformanceId { get; set; }
    public string PieceName { get; set; }
    public string PieceComposer { get; set; }
    public Period Period { get; set; }
}
