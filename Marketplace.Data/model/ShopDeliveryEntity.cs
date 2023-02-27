namespace data.model;

public class ShopDeliveryEntity:ShopDictionaryBase
{
    public ShopDeliveryEntity()
    {
        
    }
    public ShopDeliveryEntity(Guid shopId, Guid guid, double price) : base(shopId, guid)
    {
        ShopEntityId = shopId;
        DeliveryId = guid;
        Price = price;
    }
    public Guid DeliveryId { get; set; }
    public double Price { get; set; }
}