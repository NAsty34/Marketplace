using System.Net;

namespace logic.Exceptions;

public class PyementNotFoundException:BaseException
{
    public PyementNotFoundException() : base("Payment Method not found", HttpStatusCode.NotFound)
    {
    }
}