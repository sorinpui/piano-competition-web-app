namespace CompetitionApi.Domain.Entities
{
    public class Comment : EntityBase
    {
        public string Message { get; set; }

        public Rendition Rendition { get; set; } = null!;
        public User User { get; set; }
    }
}
