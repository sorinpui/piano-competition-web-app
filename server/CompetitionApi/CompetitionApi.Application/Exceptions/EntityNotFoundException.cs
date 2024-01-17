namespace CompetitionApi.Application.Exceptions
{
    public class EntityNotFoundException : ServiceException
    {
        public EntityNotFoundException() 
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }

        public EntityNotFoundException(string message) : base(message)
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }

        public EntityNotFoundException(string message, Exception inner) : base(message, inner) 
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
