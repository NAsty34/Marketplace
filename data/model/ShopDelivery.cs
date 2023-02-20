namespace data.model;

public class ShopDelivery:ShopDictionaryBase
{
    public ShopDelivery()
    {
        
    }
    public ShopDelivery(Guid shopId, Guid guid, double price) : base(shopId, guid)
    {
        ShopId = shopId;
        DeliveryId = guid;
        Price = price;
    }
    public Guid DeliveryId { get; set; }
    public double Price { get; set; }
}