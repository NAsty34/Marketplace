namespace logic.Exceptions;

public class InnAlreadyUseException:BaseException
{
    public InnAlreadyUseException():base("INN already use", 5, 400){}
}