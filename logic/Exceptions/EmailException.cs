namespace logic.Exceptions;

public class EmailException:Exception
{
    public EmailException():base("Email is already in use"){}
}