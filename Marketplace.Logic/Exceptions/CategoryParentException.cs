using System.Net;

namespace logic.Exceptions;

public class CategoryParentException:BaseException
{
    public CategoryParentException() : base("Категория не может иметь своих потомков в роли родителя", HttpStatusCode.BadRequest)
    {
    }
}