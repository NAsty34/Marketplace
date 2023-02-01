namespace data.model;

public class ShopDictionaryBase
{
    public ShopDictionaryBase()
    {
    }

    public ShopDictionaryBase(Guid shopid, Guid guid)
    {
        this.shopid = shopid;
    }
    public Guid Id { get; set; }
    public Guid shopid { get; set; }
}