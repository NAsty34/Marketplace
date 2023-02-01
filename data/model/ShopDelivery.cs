namespace data.model;

public class ShopDelivery:ShopDictionaryBase
{
    public ShopDelivery()
    {
        
    }
    public ShopDelivery(Guid shopid, Guid guid) : base(shopid, guid)
    {
        this.shopid = shopid;
        this.DeliveryId = guid;
    }
    public Guid DeliveryId { get; set; }
}