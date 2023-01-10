namespace logic.Exceptions;

public class EmailIsVerifiedException:BaseException
{
    public EmailIsVerifiedException() : base("Подтвердите почту",11, 401)
    {
    }

    
}