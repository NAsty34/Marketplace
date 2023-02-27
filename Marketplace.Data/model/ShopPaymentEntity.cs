namespace data.model;

public class ShopPaymentEntity:ShopDictionaryBase
{
    public ShopPaymentEntity()
    {
        
    }
    public ShopPaymentEntity(Guid shopId, Guid guid, double com) : base(shopId, guid)
    {
        ShopEntityId = shopId;
        PaymentId = guid;
        Commission = com;
    }


    public Guid PaymentId { get; set; }
    public double Commission { get; set; }
}