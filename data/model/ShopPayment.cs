namespace data.model;

public class ShopPayment:ShopDictionaryBase
{
    public ShopPayment()
    {
        
    }
    public ShopPayment(Guid shopid, Guid guid, double com) : base(shopid, guid)
    {
        this.shopid = shopid;
        this.Paymentid = guid;
        this.commision = com;
    }


    public Guid Paymentid { get; set; }
    public double commision { get; set; }
}