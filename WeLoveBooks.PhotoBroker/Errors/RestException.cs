using System.Net;

namespace WeLoveBooks.PhotoBroker.Errors;

public class RestException : Exception
{
    public RestException(HttpStatusCode statusCode, object errors = null)
    {
        (StatusCode, Errors) = (statusCode, errors);
    }

    public HttpStatusCode StatusCode { get; private set; }
    public object Errors { get; private set; }
}
