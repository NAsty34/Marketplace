using System.Net;

namespace logic.Exceptions;

public class BaseException:Exception
{
    public int Status { get; set; }
    

    /*public BaseException(string msg, int status) : base(msg)
    {
        Status = status;
    }*/

    public BaseException(string msg, HttpStatusCode httpStatusCode) : base(msg)
    {
        Status = 200;
    }
}