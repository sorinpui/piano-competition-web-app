using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionWebApi.Domain.Entities;

[Table("Scores")]
public class Score : EntityBase
{
    [Required]
    public int Interpretation { get; set; }

    [Required]
    public int Technicality { get; set; }

    [Required]
    public int Difficulty { get; set; }

    public int PerformanceId { get; set; }
    public Performance Performance { get; set; }
}
