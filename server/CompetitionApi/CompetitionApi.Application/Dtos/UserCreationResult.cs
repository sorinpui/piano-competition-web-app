using System.Net;

namespace CompetitionApi.Application.Dtos
{
    public class UserCreationResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool OperationSucceeded { get; set; }
        public string Message { get; set; } = null!;
    }
}
