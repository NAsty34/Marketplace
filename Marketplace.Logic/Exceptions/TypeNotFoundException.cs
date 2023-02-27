using System.Net;

namespace logic.Exceptions;

public class TypeNotFoundException:BaseException
{
    public TypeNotFoundException() : base("Type not found", HttpStatusCode.NotFound)
    {
    }
}