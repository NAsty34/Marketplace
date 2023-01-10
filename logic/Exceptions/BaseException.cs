namespace logic.Exceptions;

public class BaseException:Exception
{
    public int Status { get; set; }
    public int Code { get; set; }

    public BaseException(string msg, int code, int status) : base(msg)
    {
        Status = status;
        Code = code;
    }

    public BaseException(string msg, int code) : base(msg)
    {
        Code = code;
        Status = 200;
    }
}