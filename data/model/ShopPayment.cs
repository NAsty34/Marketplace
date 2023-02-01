namespace data.model;

public class ShopPayment:ShopDictionaryBase
{
    public ShopPayment()
    {
        
    }
    public ShopPayment(Guid shopid, Guid guid) : base(shopid, guid)
    {
        this.shopid = shopid;
        this.Paymentid = guid;
    }
    public Guid Paymentid { get; set; }
}