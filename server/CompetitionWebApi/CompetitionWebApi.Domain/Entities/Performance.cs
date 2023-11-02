using System.ComponentModel.DataAnnotations;

namespace CompetitionWebApi.Domain.Entities;

public class Performance : EntityBase
{
    [Required]
    public Piece Piece { get; set; }

    public string? VideoUri { get; set; }
    public int Likes { get; set; }
    public DateTime CreatedAt { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Comment> Comments { get; } = new List<Comment>();
}
