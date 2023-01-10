namespace logic.Exceptions;

public class RoleException:Exception
{
    public RoleException():base("Only Buyer and Seller can be register"){}
}