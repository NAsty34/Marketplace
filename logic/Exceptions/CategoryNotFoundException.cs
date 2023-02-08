using System.Net;

namespace logic.Exceptions;

public class CategoryNotFoundException:BaseException
{
    public CategoryNotFoundException() : base("Category not found", 20, HttpStatusCode.NotFound)
    {
    }
}