using System.Net;

namespace CompetitionApi.Application.Dtos
{
    public class ScoreCreationResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool OperationSucceeded { get; set; }
        public string Message { get; set; } = null!;
        public string? UriLocation { get; set; }
    }
}
