using System.Net;

namespace logic.Exceptions;

public class ParentNotFoundException:BaseException
{
    public ParentNotFoundException() : base("Parent not found", 23, HttpStatusCode.NotFound)
    {
    }
}