using System.Net;

namespace logic.Exceptions;

public class TypeDiscriptionExtencion:BaseException
{
    public TypeDiscriptionExtencion() : base("Количество символом не должно превышать 500", 30, HttpStatusCode.BadRequest)
    {
    }
}