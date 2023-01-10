namespace logic.Exceptions;

public class InnIncorrectException:BaseException
{
    public InnIncorrectException() : base("INN incorrect", 7, 400){}
}