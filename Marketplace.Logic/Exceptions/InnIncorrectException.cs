using System.Net;

namespace logic.Exceptions;

public class InnIncorrectException:BaseException
{
    public InnIncorrectException() : base("INN incorrect", HttpStatusCode.BadRequest){}
}