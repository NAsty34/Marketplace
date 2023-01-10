namespace logic.Exceptions;

public class AccessDeniedException:BaseException
{
    public AccessDeniedException():base("Access denied", 2, 403){}
}