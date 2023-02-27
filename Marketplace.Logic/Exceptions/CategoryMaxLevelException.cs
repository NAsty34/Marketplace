using System.Net;

namespace logic.Exceptions;

public class CategoryMaxLevelException:BaseException
{
    public CategoryMaxLevelException() : base("Максимальный уроыень вложенности категорий 4",HttpStatusCode.BadRequest)
    {
    }
}