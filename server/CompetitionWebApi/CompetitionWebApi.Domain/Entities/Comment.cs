using System.ComponentModel.DataAnnotations;

namespace CompetitionWebApi.Domain.Entities;

public class Comment : EntityBase
{
    [Required]
    public string Message { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    public int PerformanceId { get; set; }
    public Performance Performance { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }
}
