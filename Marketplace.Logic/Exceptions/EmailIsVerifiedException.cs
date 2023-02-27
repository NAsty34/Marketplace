using System.Net;

namespace logic.Exceptions;

public class EmailIsVerifiedException:BaseException
{
    public EmailIsVerifiedException() : base("Подтвердите почту", HttpStatusCode.Unauthorized)
    {
    }

    
}