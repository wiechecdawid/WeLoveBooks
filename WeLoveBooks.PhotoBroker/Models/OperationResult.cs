using System.Net;

namespace WeLoveBooks.PhotoBroker.Models;

public class OperationResult
{
    public OperationStatus OperationStatus { get; set; } = OperationStatus.Unresolved;
    public string Message { get; set; }
    public HttpStatusCode HttpStatusCode => OperationStatus == OperationStatus.Success ? HttpStatusCode.OK :
                (OperationStatus == OperationStatus.Failure ? HttpStatusCode.BadRequest :
                    HttpStatusCode.InternalServerError);
}

public enum OperationStatus
{
    Success,
    Failure,
    Unresolved
}
