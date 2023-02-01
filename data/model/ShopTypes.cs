namespace data.model;

public class ShopTypes:ShopDictionaryBase
{
    public ShopTypes()
    {
        
    }
    public ShopTypes(Guid shopid, Guid guid) : base(shopid, guid)
    {
        this.shopid = shopid;
        this.TypeId = guid;
    }
    public Guid TypeId { get; set; }
}