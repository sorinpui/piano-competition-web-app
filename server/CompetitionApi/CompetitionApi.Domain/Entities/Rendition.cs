using CompetitionApi.Domain.ComplexTypes;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionApi.Domain.Entities
{
    public class Rendition : EntityBase
    {
        public Piece Piece { get; set; }
        public string VideoUrl { get; set; }

        [ForeignKey("PerformerId")]
        public User User { get; set; } = null!;
        public Score? Score { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
