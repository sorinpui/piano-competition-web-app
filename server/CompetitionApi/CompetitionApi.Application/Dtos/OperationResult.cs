using System.Net;

namespace CompetitionApi.Application.Dtos
{
    public class OperationResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool OperationSucceeded { get; set; }
        public string Message { get; set; }
        public T? Payload { get; set; }
    }
}
