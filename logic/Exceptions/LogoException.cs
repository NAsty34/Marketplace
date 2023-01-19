using data.model;

namespace logic.Exceptions;

public class LogoException:BaseException
{
    public LogoException() : base("Неверный формат файла", 20, 400){}
}