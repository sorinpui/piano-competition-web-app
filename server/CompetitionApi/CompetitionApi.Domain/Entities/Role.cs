using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionApi.Domain.Entities
{
    public class Role : EntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
