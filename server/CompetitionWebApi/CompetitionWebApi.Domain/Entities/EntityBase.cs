using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionWebApi.Domain.Entities;

public abstract class EntityBase
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual int Id { get; set; }
}
