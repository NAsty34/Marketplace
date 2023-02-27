using System.Net;

namespace logic.Exceptions;

public class RoleException:BaseException
{
    public RoleException():base("Only Buyer and Seller can be register", HttpStatusCode.Forbidden){}
}