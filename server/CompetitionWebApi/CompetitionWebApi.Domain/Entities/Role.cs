using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionWebApi.Domain.Entities;

[Table("Roles")]
public class Role : EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public override int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public List<User> Users { get; set; } = new();
}
