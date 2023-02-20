namespace data.model;

public class ShopPayment:ShopDictionaryBase
{
    public ShopPayment()
    {
        
    }
    public ShopPayment(Guid shopId, Guid guid, double com) : base(shopId, guid)
    {
        ShopId = shopId;
        PaymentId = guid;
        Commission = com;
    }


    public Guid PaymentId { get; set; }
    public double Commission { get; set; }
}