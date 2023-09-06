using System.ComponentModel.DataAnnotations;

namespace CompetitionWebApi.Domain.Entities;

public class Performance : EntityBase
{
    [Required]
    public Piece Piece { get; set; }

    public string VideoUri { get; set; }

    [Required]
    public int UserId { get; set; }
    public User User { get; set; }
}
