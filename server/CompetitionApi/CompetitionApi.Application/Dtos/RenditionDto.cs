namespace CompetitionApi.Application.Dtos
{
    public class RenditionDto : RenditionSummaryDto
    {
        public int? Interpretation { get; set; }
        public int? Difficulty { get; set; }
        public int? Technique { get; set; }

        public double? OverallScore { get; set; }

    }
}
