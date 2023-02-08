using System.Net;

namespace logic.Exceptions;

public class PasswordIncorrectException:BaseException
{
    public PasswordIncorrectException() : base("Неверный пароль", 12, HttpStatusCode.Forbidden)
    {
    }

    
}