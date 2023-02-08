using System.Net;

namespace logic.Exceptions;

public class ShopNotFoundException:BaseException
{
    public ShopNotFoundException():base("Shop not found", 9, HttpStatusCode.NotFound){}
}