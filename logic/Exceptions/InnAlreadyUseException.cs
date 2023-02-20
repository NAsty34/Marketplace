using System.Net;

namespace logic.Exceptions;

public class InnAlreadyUseException:BaseException
{
    public InnAlreadyUseException():base("INN already use", HttpStatusCode.BadRequest){}
}