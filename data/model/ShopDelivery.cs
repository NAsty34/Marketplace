namespace data.model;

public class ShopDelivery:ShopDictionaryBase
{
    public ShopDelivery()
    {
        
    }
    public ShopDelivery(Guid shopid, Guid guid, double price) : base(shopid, guid)
    {
        this.shopid = shopid;
        this.DeliveryId = guid;
        this.Price = price;
    }
    public Guid DeliveryId { get; set; }
    public double Price { get; set; }
}