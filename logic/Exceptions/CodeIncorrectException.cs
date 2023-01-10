namespace logic.Exceptions;

public class CodeIncorrectException:BaseException
{
    public CodeIncorrectException() : base("Неверный код", 13, 403)
    {
    }

  
}