using System.Net;

namespace logic.Exceptions;

public class ProductUseException:BaseException
{
    public ProductUseException() : base("Продукт существует", HttpStatusCode.BadRequest)
    {
    }
}