using System.Net;

namespace logic.Exceptions;

public class UserNotFoundException:BaseException
{
    public UserNotFoundException():base("User not found", HttpStatusCode.NotFound){}
}