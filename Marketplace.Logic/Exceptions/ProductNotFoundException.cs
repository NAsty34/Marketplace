using System.Net;

namespace logic.Exceptions;

public class ProductNotFoundException:BaseException
{
    public ProductNotFoundException() : base("Продукт не найден", HttpStatusCode.NotFound)
    {
    }
}