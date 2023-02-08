using System.Net;

namespace logic.Exceptions;

public class TypeNotFoundException:BaseException
{
    public TypeNotFoundException() : base("Type not found", 24, HttpStatusCode.NotFound)
    {
    }
}