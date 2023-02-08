using System.Net;

namespace logic.Exceptions;

public class CommissionNotUseException:BaseException
{
    public CommissionNotUseException() : base("Коммисия не подходит", 26, HttpStatusCode.BadRequest)
    {
    }
}