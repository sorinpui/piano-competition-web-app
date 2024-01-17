namespace CompetitionApi.Application.Requests
{
    public class CreateScoreRequest
    {
        public int Interpretation { get; set; }
        public int Difficulty { get; set; }
        public int Technique { get; set; }
        public int RenditionId { get; set; }
    }
}
