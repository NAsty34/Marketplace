namespace logic.Exceptions;

public class UserNotFoundException:BaseException
{
    public UserNotFoundException():base("User not found", 10, 404){}
}