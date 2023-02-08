using System.Net;

namespace logic.Exceptions;

public class CategoryParentCategoryException:BaseException
{
    public CategoryParentCategoryException() : base("Потомок категории сама категория", 22, HttpStatusCode.BadRequest)
    {
    }
}