using System.ComponentModel.DataAnnotations;

namespace CompetitionApi.Domain.Entities
{
    public class User : EntityBase
    {
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public List<Role> Roles { get; set; }
    }
}
