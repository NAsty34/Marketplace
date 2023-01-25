namespace data.model;

public class ShopDictionaryBase:BaseEntity
{
    public ShopDictionaryBase()
    {
    }

    public ShopDictionaryBase(Guid shopid, Guid guid)
    {
        this.shopid = shopid;
    }
    public Guid shopid { get; set; }
}