namespace data.model;

public class ShopCategoryEntity:ShopDictionaryBase
{
    public ShopCategoryEntity()
    {
        
    }
    public ShopCategoryEntity(Guid shopId, Guid guid) : base(shopId, guid)
    {
        ShopEntityId = shopId;
        CategoryId = guid;
    }

    
    public Guid CategoryId { get; set; } 
}