namespace CompetitionApi.Application.Requests
{
    public class PostCommentRequest
    {
        public string Message { get; set; }
        public int RenditionId { get; set; }
    }
}
