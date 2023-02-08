using System.Net;

namespace logic.Exceptions;

public class AccessDeniedException:BaseException
{
    public AccessDeniedException():base("Access denied", 2, HttpStatusCode.Forbidden){}
}