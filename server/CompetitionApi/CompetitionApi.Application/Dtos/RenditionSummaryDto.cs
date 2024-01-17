namespace CompetitionApi.Application.Dtos
{
    public class RenditionSummaryDto
    {
        public int RenditionId { get; set; }
        public string Performer {  get; set; }
        public string Piece { get; set; }
        public string Composer { get; set; }
        public string Period { get; set; }
    }
}
