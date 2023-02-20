namespace data.model;

public class ShopTypes:ShopDictionaryBase
{
    public ShopTypes()
    {
        
    }
    public ShopTypes(Guid shopId, Guid guid) : base(shopId, guid)
    {
        ShopId = shopId;
        TypeId = guid;
    }
    public Guid TypeId { get; set; }
}