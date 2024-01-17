using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionApi.Domain.Entities
{
    public class Score : EntityBase
    {
        [Range(0, 10)]
        public int Interpretation {  get; set; }

        [Range(0, 10)]
        public int Difficulty { get; set; }

        [Range(0, 10)]
        public int Technique { get; set; }

        public int RenditionId { get; set; }
        public Rendition Rendition { get; set; } = null!;


        [ForeignKey("JudgeId")]
        public User User { get; set; }
    }
}
