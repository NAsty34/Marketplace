using System.Net;

namespace logic.Exceptions;

public class CodeIncorrectException:BaseException
{
    public CodeIncorrectException() : base("Неверный код", 13, HttpStatusCode.Forbidden)
    {
    }

  
}