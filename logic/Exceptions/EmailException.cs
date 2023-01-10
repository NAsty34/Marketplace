namespace logic.Exceptions;

public class EmailException:BaseException
{
    public EmailException():base("Email is already in use", 3, 400){}
}