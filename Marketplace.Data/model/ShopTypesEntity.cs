namespace data.model;

public class ShopTypesEntity:ShopDictionaryBase
{
    public ShopTypesEntity()
    {
        
    }
    public ShopTypesEntity(Guid shopId, Guid guid) : base(shopId, guid)
    {
        ShopEntityId = shopId;
        TypeId = guid;
    }
    public Guid TypeId { get; set; }
}