using CompetitionApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompetitionApi.Domain.ComplexTypes
{
    [ComplexType]
    public class Piece
    {
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Composer { get; set; }

        public Period Period { get; set; }
    }
}
