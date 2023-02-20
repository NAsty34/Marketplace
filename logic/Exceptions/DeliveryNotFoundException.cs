using System.Net;

namespace logic.Exceptions;

public class DeliveryNotFoundException:BaseException
{
    public DeliveryNotFoundException() : base("Delivery Type not found", HttpStatusCode.NotFound)
    {
    }
}